using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site
{
    public partial class One_page : System.Web.UI.Page
    {
        // Флаг: разрешено ли чтение Excel из БД (для локальной версии — false)
        private const bool USE_EXCEL = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "width", "width();", true);

            // Устанавливаем имя объекта из URL
            if (!IsPostBack)
            {
                if (Request.QueryString["map"] != null)
                {
                    Label1.Text = Request.QueryString["map"];
                }
            }

            // Заполняем скрытый CheckBoxList объектами (для совместимости)
            LoadObjectCheckbox();

            // Галерея изображений
            LoadObjectImages();

            // Привязываем все GridView
            BindAllTables();

            // Скрываем все панели по умолчанию
            skrit();

            // Стартовое состояние — «Основная информация» раскрыта
            if (!IsPostBack)
            {
                Chk_view.Items[0].Selected = true;
                btn_osnov_inf.ImageUrl = "/Logo/Стрелка.svg";
                Panel_osnova.CssClass = "panel_click";
                Osnov_inf.CssClass = "inf_otch_click";
                btn_dow_inf.Visible = true;
            }

            chk();
        }

        // =====================================================================
        // ЗАГРУЗКА СПИСКА ОБЪЕКТОВ
        // =====================================================================

        private void LoadObjectCheckbox()
        {
            Check_Object.DataSource = LocalData.GetObjects()
                .Where(o => !string.IsNullOrEmpty(o.NameObject))
                .Select(o => new { o.NameObject })
                .OrderBy(o => o.NameObject)
                .ToList();
            Check_Object.DataTextField = "NameObject";
            Check_Object.DataValueField = "NameObject";
            Check_Object.DataBind();
        }

        private void LoadObjectImages()
        {
            var images = LocalData.GetObjectImages(Label1.Text);
            rptMarkers.DataSource = images;
            rptMarkers.DataBind();
        }

        // =====================================================================
        // ПРИВЯЗКА ВСЕХ ТАБЛИЦ
        // =====================================================================

        private void BindAllTables()
        {
            string nameObject = Label1.Text;
            if (string.IsNullOrEmpty(nameObject)) return;

            var obj = LocalData.GetObjects()
                .FirstOrDefault(o => o.NameObject == nameObject);

            if (obj == null)
            {
                // Объект не найден — скрываем таблицы
                return;
            }

            // === Основная информация ===
            var info = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Имя объекта", obj.NameObject),
                new KeyValuePair<string, string>("Адрес", obj.Adress),
                new KeyValuePair<string, string>("Округ", obj.District),
                new KeyValuePair<string, string>("Район", obj.Raion),
                new KeyValuePair<string, string>("Индекс", obj.Indexx),
                new KeyValuePair<string, string>("Статус", obj.Statuss ?? "—")
            };
            Table_inf.DataSource = ToDataTableTransposed(info);
            Table_inf.DataBind();

            // === Сведения о зданиях/сооружениях ===
            var buildings = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Имя объекта", obj.NameObject),
                new KeyValuePair<string, string>("Кадастровый номер здания/ЗУ", obj.CadastralNumber ?? "—"),
                new KeyValuePair<string, string>("Год постройки", obj.ConstructionYear ?? "—"),
                new KeyValuePair<string, string>("Этажность", obj.Levels ?? "—"),
                new KeyValuePair<string, string>("Дата ввода в эксплуатацию", obj.CommissioningDate ?? "—"),
                new KeyValuePair<string, string>("Площадь зданий и сооружений", obj.SqureBuildings ?? "—")
            };
            Table_kat.DataSource = ToDataTableTransposed(buildings);
            Table_kat.DataBind();

            // === Земельный участок ===
            var land = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Имя объекта", obj.NameObject),
                new KeyValuePair<string, string>("Зеленые насаждения", obj.GreenSpaces ?? "—"),
                new KeyValuePair<string, string>("Площадь территорий", obj.SqureTerritory ?? "—"),
                new KeyValuePair<string, string>("Используемая территория", obj.TerritoriesUsed ?? "—")
            };
            Table_sved.DataSource = ToDataTableTransposed(land);
            Table_sved.DataBind();

            // === Инженерные системы ===
            var eng = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Имя объекта", obj.NameObject),
                new KeyValuePair<string, string>("Показатель 1", obj.Electrosnab ?? "—"),
                new KeyValuePair<string, string>("Показатель 2", obj.Vodsnab ?? "—"),
                new KeyValuePair<string, string>("Показатель 3", obj.OZDS ?? "—")
            };
            Table_ing.DataSource = ToDataTableTransposed(eng);
            Table_ing.DataBind();

            // === Прочие данные ===
            var other = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Имя объекта", obj.NameObject),
                new KeyValuePair<string, string>("Площадь убираемых помещений", "—"),
                new KeyValuePair<string, string>("Площадь убираемой территории", "—"),
                new KeyValuePair<string, string>("Количество зон приема посетителей", "—"),
                new KeyValuePair<string, string>("Количество аппаратов ККТ", "—"),
                new KeyValuePair<string, string>("Количество крючков в гардеробе", "—"),
                new KeyValuePair<string, string>("Наличие специальной техники", "—")
            };
            Table_proch.DataSource = ToDataTableTransposed(other);
            Table_proch.DataBind();

            // === Документы ===
            var doc = LocalData.GetObjectDocuments()
                .FirstOrDefault(d => d.NameObject == nameObject);

            if (doc != null)
            {
                // ОКС
                var oks = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("Имя объекта", doc.NameObject),
                    new KeyValuePair<string, string>("Документ 1", doc.OrderOKS),
                    new KeyValuePair<string, string>("Документ 2", doc.AktPriemStroi),
                    new KeyValuePair<string, string>("Документ 3", doc.ExtractOKS)
                };
                GridView_OKS.DataSource = ToDataTableTransposed(oks);
                GridView_OKS.DataBind();

                // ЗУ
                var zu = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("Имя объекта", doc.NameObject),
                    new KeyValuePair<string, string>("Распоряжение / (РДГИ)", doc.OrderZU),
                    new KeyValuePair<string, string>("Договор БП или аренды", doc.Contract),
                    new KeyValuePair<string, string>("Выписка из ЕГРН", doc.ExtractZU),
                    new KeyValuePair<string, string>("Прекращение права", doc.TerminationZU)
                };
                GridView_ZU.DataSource = ToDataTableTransposed(zu);
                GridView_ZU.DataBind();

                // Техническая документация
                var tex = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("Имя объекта", doc.NameObject),
                    new KeyValuePair<string, string>("Технический паспорт", doc.TexPasport),
                    new KeyValuePair<string, string>("Экспликации", doc.Eksplikation),
                    new KeyValuePair<string, string>("Поэтажные планы", doc.PoetapPlan),
                    new KeyValuePair<string, string>("Планы территории", doc.TerritoryPlans)
                };
                Table_tex.DataSource = ToDataTableTransposed(tex);
                Table_tex.DataBind();
            }

            // === Спортивные зоны ===
            var zones = LocalData.GetSportsZones()
                .Where(z => z.NameObject == nameObject)
                .ToList();

            if (zones.Count > 0)
            {
                ListSports_zone.DataSource = zones.Select(z => new { z.NameZone }).ToList();
                ListSports_zone.DataTextField = "NameZone";
                ListSports_zone.DataValueField = "NameZone";
                ListSports_zone.DataBind();

                Table_zone.DataSource = zones.Select(z => new
                {
                    NameZone = z.NameZone,
                    Square = z.Square,
                    Size = z.Size,
                    ZoneType = z.ZoneType,
                    TypeComp = z.TypeComp,
                    Coating = z.Coating,
                    ChangingRooms = z.ChangingRooms,
                    PossibleUse = z.PossibleUse,
                    SportsEquipment = z.SportsEquipment,
                    EPS = z.EPS,
                    EPS_IAS = z.EPS_IAS,
                    UrlImage = z.UrlImage
                }).ToList();
                Table_zone.DataBind();
            }
        }

        // =====================================================================
        // ТРАНСПОНИРОВАНИЕ СПИСКА В DataTable
        // =====================================================================

        /// <summary>
        /// Превращает список KeyValuePair в DataTable с двумя колонками:
        /// «Поле» и «Значение». Каждый KeyValuePair — отдельная строка.
        /// </summary>
        private static DataTable ToDataTableTransposed(List<KeyValuePair<string, string>> items)
        {
            var dt = new DataTable();
            dt.Columns.Add("Поле");
            dt.Columns.Add("Значение");
            foreach (var item in items)
            {
                var row = dt.NewRow();
                row["Поле"] = item.Key;
                row["Значение"] = item.Value ?? "";
                dt.Rows.Add(row);
            }
            return dt;
        }

        // =====================================================================
        // «ЛЕГАСИ»-МЕТОД: транспонирование DataTable (нужен для GridView1)
        // =====================================================================

        private DataTable GenerateTransposedTable(DataTable inputTable)
        {
            DataTable outputTable = new DataTable();
            outputTable.Columns.Add(inputTable.Columns[0].ColumnName.ToString());

            foreach (DataRow inRow in inputTable.Rows)
            {
                string newColName = inRow[0].ToString();
                outputTable.Columns.Add(newColName);
            }

            for (int rCount = 1; rCount <= inputTable.Columns.Count - 1; rCount++)
            {
                DataRow newRow = outputTable.NewRow();
                newRow[0] = inputTable.Columns[rCount].ColumnName.ToString();
                for (int cCount = 0; cCount <= inputTable.Rows.Count - 1; cCount++)
                {
                    string colValue = inputTable.Rows[cCount][rCount].ToString();
                    newRow[cCount + 1] = colValue;
                }
                outputTable.Rows.Add(newRow);
            }

            return outputTable;
        }
        // =====================================================================
        // ВСПОМОГАТЕЛЬНЫЕ МЕТОДЫ
        // =====================================================================

        /// <summary>Задаёт ширину столбцов таблицы.</summary>
        public void shirina(GridView grid)
        {
            foreach (GridViewRow row in grid.Rows)
            {
                int count = 0;
                foreach (TableCell cell in row.Cells)
                {
                    if (Server.HtmlDecode(cell.Text) != "")
                    {
                        cell.Width = 150;
                        count++;
                    }
                }
                if (row.Cells.Count > 0)
                {
                    row.Cells[0].Width = 200;
                    row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
                }
            }
        }

        /// <summary>Скрывает все панели и кнопки скачивания.</summary>
        public void skrit()
        {
            Vidim.Visible = false;
            btn_dow_inf.Visible = false;
            btn_dow_cat.Visible = false;
            btn_dow_sved.Visible = false;
            btn_dow_ing.Visible = false;
            btn_dow_tex.Visible = false;
            btn_dow_in.Visible = false;
            btn_dow_proch.Visible = false;
            btn_dow_zone.Visible = false;
            Vibrat.Visible = false;
            Snat.Visible = false;
            Zone.Visible = false;
        }

        /// <summary>Скрывает все таблицы.</summary>
        public void skrit_vse_table()
        {
            skrit_table(Table_inf);
            skrit_table(Table_kat);
            skrit_table(Table_sved);
            skrit_table(Table_ing);
            skrit_table(Table_tex);
            skrit_table(GridView_OKS);
            skrit_table(GridView_ZU);
            skrit_table(Table_in);
            skrit_table(Table_proch);
            skrit_table(GridView1);
        }

        public void skrit_table(GridView grid)
        {
            grid.Visible = false;
        }

        /// <summary>Отображает нужную таблицу.</summary>
        public void pokaz_table(GridView grid)
        {
            foreach (ListItem chk in Check_Object.Items)
            {
                if (chk.Selected)
                {
                    grid.Visible = true;
                    foreach (GridViewRow row in grid.Rows)
                    {
                        if (row.Cells.Count > 1 &&
                            row.Cells[1].Text != "&nbsp;" && row.Cells[1].Text != "")
                        {
                            row.Visible = true;
                            row.Cells[0].Visible = true;
                            row.Cells[1].Visible = true;
                        }
                        else
                        {
                            row.Visible = false;
                        }
                    }
                }
            }
        }

        /// <summary>Отображает видимую часть таблицы (для скачивания).</summary>
        public void pokaz_table_diapazon(GridView grid, int min, int max)
        {
            foreach (ListItem chk in Check_Object.Items)
            {
                if (chk.Selected)
                {
                    if (grid.HeaderRow == null) continue;
                    foreach (TableCell cell in grid.HeaderRow.Cells)
                    {
                        if (cell.Text.Replace("&quot;", "\u0022") == chk.Value.Trim())
                        {
                            foreach (GridViewRow row in grid.Rows)
                            {
                                if ((row.RowIndex >= min) & (row.RowIndex <= max))
                                {
                                    if (row.Cells[1].Text.Length > 1)
                                    {
                                        row.Visible = true;
                                        row.Cells[0].Visible = true;
                                        row.Cells[1].Visible = true;
                                    }
                                    else
                                    {
                                        row.Visible = false;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>Показывает таблицы, соответствующие выбранным CheckBox'ам.</summary>
        public void chk()
        {
            foreach (ListItem check in Chk_view.Items)
            {
                if (!check.Selected) continue;

                switch (check.Text)
                {
                    case "Основная информация":
                        btn_dow_inf.Visible = true;
                        pokaz_table(Table_inf);
                        break;
                    case "Сведения о зданиях/сооружениях":
                        btn_dow_cat.Visible = true;
                        pokaz_table(Table_kat);
                        break;
                    case "Сведения о земельном участке":
                        btn_dow_sved.Visible = true;
                        pokaz_table(Table_sved);
                        break;
                    case "Инженерные системы":
                        btn_dow_ing.Visible = true;
                        pokaz_table(Table_ing);
                        break;
                    case "Документы":
                        btn_dow_tex.Visible = true;
                        pokaz_table(Table_tex);
                        pokaz_table(GridView_OKS);
                        pokaz_table(GridView_ZU);
                        break;
                    case "Иная документация":
                        btn_dow_in.Visible = true;
                        pokaz_table(Table_in);
                        break;
                    case "Прочие данные":
                        btn_dow_proch.Visible = true;
                        pokaz_table(Table_proch);
                        break;
                    case "Спортивные зоны":
                        Vibrat.Visible = true;
                        Snat.Visible = true;
                        Zone.Visible = true;
                        pokaz_table_chk(Table_zone);
                        break;
                    case "Прейскурант":
                        btn_tabl_tabl.Visible = true;
                        pokaz_table(Tabl_tabl);
                        break;
                }
            }

            // Показываем кнопку «Скачать видимые данные», если хоть одна таблица видима
            if (btn_dow_inf.Visible | btn_dow_cat.Visible | btn_dow_sved.Visible | btn_dow_ing.Visible
                | btn_dow_tex.Visible | btn_dow_in.Visible | btn_dow_proch.Visible
                | (Btn_sports_zone.ImageUrl == "/Logo/Стрелка.svg"))
            {
                Vidim.Visible = true;
            }
            else
            {
                Vidim.Visible = false;
            }
        }

        /// <summary>Отображает спортзоны, выбранные в списке.</summary>
        public void pokaz_table_chk(GridView grid)
        {
            int count = 0;
            foreach (ListItem chkItem in ListSports_zone.Items)
            {
                if (chkItem.Selected)
                {
                    count++;
                    grid.HeaderRow.Visible = true;
                    btn_dow_zone.Visible = true;
                    foreach (GridViewRow row in grid.Rows)
                    {
                        if (row.Cells[1].Text == chkItem.Text) row.Visible = true;
                        if (row.Cells.Count > 12) row.Cells[12].Visible = false;
                    }
                    if (grid.HeaderRow.Cells.Count > 12) grid.HeaderRow.Cells[12].Visible = false;
                }
                else
                {
                    foreach (GridViewRow row in grid.Rows)
                    {
                        if (row.Cells[1].Text == chkItem.Text) row.Visible = false;
                    }
                    if (count == 0) btn_dow_zone.Visible = false;
                }
            }
        }
        // =====================================================================
        // РАСКРЫТИЕ / СВОРАЧИВАНИЕ ПАНЕЛЕЙ
        // =====================================================================

        /// <summary>Основная информация</summary>
        protected void btn_osnov_inf_Click(object sender, ImageClickEventArgs e)
        {
            TogglePanel(Chk_view, 0, btn_osnov_inf, Panel_osnova, Osnov_inf, btn_dow_inf, Table_inf);
            UpdateVidimVisibility();
        }

        /// <summary>Сведения о зданиях/сооружениях</summary>
        protected void btn_kat_Click(object sender, ImageClickEventArgs e)
        {
            TogglePanel(Chk_view, 1, btn_kat, Panel_kat, Kategor, btn_dow_cat, Table_kat);
            UpdateVidimVisibility();
        }

        /// <summary>Инженерные системы</summary>
        protected void btn_inj_sys_Click(object sender, ImageClickEventArgs e)
        {
            TogglePanel(Chk_view, 2, btn_inj_sys, Panel_inj, Ing_sys, btn_dow_ing, Table_ing);
            UpdateVidimVisibility();
        }

        /// <summary>Документы</summary>
        protected void btn_tex_doc_Click(object sender, ImageClickEventArgs e)
        {
            TogglePanel(Chk_view, 3, btn_tex_doc, Panel_tex, Tex_doc, btn_dow_tex, Table_tex);

            if (Chk_view.Items[3].Selected)
            {
                Label_OKS.Visible = true;
                Label_ZU.Visible = true;
                Label_Tex.Visible = true;
                pokaz_table(GridView_ZU);
                pokaz_table(GridView_OKS);
            }
            else
            {
                Label_OKS.Visible = false;
                Label_ZU.Visible = false;
                Label_Tex.Visible = false;
                skrit_table(GridView_ZU);
                skrit_table(GridView_OKS);
            }
            UpdateVidimVisibility();
        }

        /// <summary>Иная документация (пока скрыта)</summary>
        protected void btn_in_doc_Click(object sender, ImageClickEventArgs e)
        {
            TogglePanel(Chk_view, 4, btn_in_doc, Panel_in, In_doc, btn_dow_in, Table_in);
            UpdateVidimVisibility();
        }

        /// <summary>Прочие данные</summary>
        protected void btn_proch_inf_Click(object sender, ImageClickEventArgs e)
        {
            TogglePanel(Chk_view, 5, btn_proch_inf, Panel_proch, Proch_doc, btn_dow_proch, Table_proch);
            UpdateVidimVisibility();
        }

        /// <summary>Спортивные зоны</summary>
        protected void Btn_sports_zone_Click(object sender, ImageClickEventArgs e)
        {
            Chk_view.Items[6].Selected = !Chk_view.Items[6].Selected;

            if (Chk_view.Items[6].Selected)
            {
                Btn_sports_zone.ImageUrl = "/Logo/Стрелка.svg";
                Panel_sport_zone.CssClass = "panel_click";
                Sports_zone.CssClass = "inf_otch_click";
                Vibrat.Visible = true;
                Snat.Visible = true;
                Zone.Visible = true;
                pokaz_table_chk(Table_zone);
            }
            else
            {
                Btn_sports_zone.ImageUrl = "/Logo/Стрелка вправо.svg";
                Panel_sport_zone.CssClass = "panel_state";
                Sports_zone.CssClass = "inf_otch";
                btn_dow_zone.Visible = false;
                ListSports_zone.ClearSelection();
                Vibrat.Visible = false;
                Snat.Visible = false;
                Zone.Visible = false;
            }
            UpdateVidimVisibility();
        }

        /// <summary>Прейскурант</summary>
        protected void btn_tabl_Click(object sender, ImageClickEventArgs e)
        {
            Chk_view.Items[7].Selected = !Chk_view.Items[7].Selected;

            if (Chk_view.Items[7].Selected)
            {
                btn_tabl_inf.ImageUrl = "/Logo/Стрелка.svg";
                Panel_tabl.CssClass = "panel_click";
                label_tabl.CssClass = "inf_otch_click";
                btn_tabl_tabl.Visible = true;

                ReadExcel();

                pokaz_table(Tabl_tabl);
            }
            else
            {
                btn_tabl_inf.ImageUrl = "/Logo/Стрелка вправо.svg";
                Panel_tabl.CssClass = "panel_state";
                label_tabl.CssClass = "inf_otch";
                btn_tabl_tabl.Visible = false;
                label_if_empty.Visible = false;
                skrit_table(Tabl_tabl);
            }
            UpdateVidimVisibility();
        }

        /// <summary>Сведения о земельном участке</summary>
        protected void btn_sved_Click(object sender, ImageClickEventArgs e)
        {
            TogglePanel(Chk_view, 8, btn_sved, Panel_sved, Svedenia, btn_dow_sved, Table_sved);
            UpdateVidimVisibility();
        }

        // =====================================================================
        // ОБЩИЙ МЕТОД РАСКРЫТИЯ/СВОРАЧИВАНИЯ
        // =====================================================================

        /// <summary>
        /// Переключает состояние панели: раскрывает или сворачивает.
        /// </summary>
        private void TogglePanel(
            CheckBoxList chkList, int index,
            ImageButton arrowButton,
            Panel panel,
            Label title,
            ImageButton downloadButton,
            GridView table)
        {
            chkList.Items[index].Selected = !chkList.Items[index].Selected;

            if (chkList.Items[index].Selected)
            {
                arrowButton.ImageUrl = "/Logo/Стрелка.svg";
                panel.CssClass = "panel_click";
                title.CssClass = "inf_otch_click";
                downloadButton.Visible = true;
                pokaz_table(table);
            }
            else
            {
                arrowButton.ImageUrl = "/Logo/Стрелка вправо.svg";
                panel.CssClass = "panel_state";
                title.CssClass = "inf_otch";
                downloadButton.Visible = false;
                skrit_table(table);
            }
        }

        /// <summary>Обновляет видимость кнопки «Скачать видимые данные».</summary>
        private void UpdateVidimVisibility()
        {
            if (btn_dow_inf.Visible | btn_dow_cat.Visible | btn_dow_sved.Visible | btn_dow_ing.Visible
                | btn_dow_tex.Visible | btn_dow_in.Visible | btn_dow_proch.Visible
                | btn_tabl_tabl.Visible
                | (Btn_sports_zone.ImageUrl == "/Logo/Стрелка.svg"))
            {
                Vidim.Visible = true;
            }
            else
            {
                Vidim.Visible = false;
            }
        }

        // =====================================================================
        // РАЗВЕРНУТЬ ВСЁ / СВЕРНУТЬ ВСЁ
        // =====================================================================

        protected void Razvernyt_Click(object sender, EventArgs e)
        {
            foreach (ListItem chk in Chk_view.Items) chk.Selected = true;

            SetAllPanelsState(expanded: true);

            Vibrat.Visible = true;
            Snat.Visible = true;
            Zone.Visible = true;

            chk();
        }

        protected void Svernyt_Click(object sender, EventArgs e)
        {
            foreach (ListItem chk in Chk_view.Items) chk.Selected = false;

            SetAllPanelsState(expanded: false);

            Vibrat.Visible = false;
            Snat.Visible = false;
            Zone.Visible = false;
            ListSports_zone.ClearSelection();

            skrit_vse_table();
        }

        /// <summary>Раскрывает или сворачивает все панели разом.</summary>
        private void SetAllPanelsState(bool expanded)
        {
            string arrow = expanded ? "/Logo/Стрелка.svg" : "/Logo/Стрелка вправо.svg";
            string cssPanel = expanded ? "panel_click" : "panel_state";
            string cssTitle = expanded ? "inf_otch_click" : "inf_otch";

            btn_osnov_inf.ImageUrl = arrow;
            Panel_osnova.CssClass = cssPanel;
            Osnov_inf.CssClass = cssTitle;
            btn_dow_inf.Visible = expanded;

            btn_kat.ImageUrl = arrow;
            Panel_kat.CssClass = cssPanel;
            Kategor.CssClass = cssTitle;
            btn_dow_cat.Visible = expanded;

            btn_sved.ImageUrl = arrow;
            Panel_sved.CssClass = cssPanel;
            Svedenia.CssClass = cssTitle;
            btn_dow_sved.Visible = expanded;

            btn_inj_sys.ImageUrl = arrow;
            Panel_inj.CssClass = cssPanel;
            Ing_sys.CssClass = cssTitle;
            btn_dow_ing.Visible = expanded;

            btn_tex_doc.ImageUrl = arrow;
            Panel_tex.CssClass = cssPanel;
            Tex_doc.CssClass = cssTitle;
            btn_dow_tex.Visible = expanded;

            btn_in_doc.ImageUrl = arrow;
            Panel_in.CssClass = cssPanel;
            In_doc.CssClass = cssTitle;
            btn_dow_in.Visible = expanded;

            btn_proch_inf.ImageUrl = arrow;
            Panel_proch.CssClass = cssPanel;
            Proch_doc.CssClass = cssTitle;
            btn_dow_proch.Visible = expanded;

            Btn_sports_zone.ImageUrl = arrow;
            Panel_sport_zone.CssClass = cssPanel;
            Sports_zone.CssClass = cssTitle;

            btn_tabl_inf.ImageUrl = arrow;
            Panel_tabl.CssClass = cssPanel;
            label_tabl.CssClass = cssTitle;
        }

        // =====================================================================
        // СПОРТИВНЫЕ ЗОНЫ: ВЫБРАТЬ / СНЯТЬ ВСЕ
        // =====================================================================

        protected void Vibrat_Click(object sender, EventArgs e)
        {
            foreach (ListItem zone in ListSports_zone.Items) zone.Selected = true;
            pokaz_table_chk(Table_zone);
        }

        protected void Snat_Click(object sender, EventArgs e)
        {
            foreach (ListItem zone in ListSports_zone.Items) zone.Selected = false;
            pokaz_table_chk(Table_zone);
        }

        // =====================================================================
        // КНОПКИ «ПОКАЗАТЬ НА КАРТЕ» / «ВЫБРАТЬ ОБЪЕКТ»
        // =====================================================================

        protected void Check_Object_DataBound(object sender, EventArgs e)
        {
            string map = Request.QueryString["map"];
            if (string.IsNullOrEmpty(map)) return;

            foreach (ListItem chkn in Check_Object.Items)
            {
                if (chkn.Value == map) chkn.Selected = true;
            }
        }

        protected void OpenMaps_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Default.aspx?map=" + Request.QueryString["map"]);
        }
        // =====================================================================
        // ПРЕЙСКУРАНТ (заглушка — Excel недоступен без БД)
        // =====================================================================

        /// <summary>
        /// В локальной версии Excel-прейскурант недоступен.
        /// Показывает сообщение «Прейскуранта нет» и скрывает кнопку скачивания.
        /// </summary>
        public void ReadExcel()
        {
            label_if_empty.Visible = true;
            btn_tabl_tabl.Visible = false;
        }

        // =====================================================================
        // СКАЧИВАНИЕ: полный отчёт
        // =====================================================================

        protected void Ves_otchet_Click(object sender, EventArgs e)
        {
            BuildFullReport();
            pokaz_table(GridView1);
            download(GridView1, "Отчёт_по_" + Label1.Text);
            skrit_table(GridView1);
        }

        /// <summary>
        /// Собирает «полный отчёт» из данных объекта в GridView1.
        /// </summary>
        private void BuildFullReport()
        {
            string nameObject = Label1.Text;
            var obj = LocalData.GetObjects().FirstOrDefault(o => o.NameObject == nameObject);

            if (obj == null)
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                return;
            }

            // Собираем плоский список всех полей
            var items = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Имя объекта", obj.NameObject),
                new KeyValuePair<string, string>("Статус", obj.Statuss ?? "—"),
                new KeyValuePair<string, string>("Адрес", obj.Adress ?? "—"),
                new KeyValuePair<string, string>("Округ", obj.District ?? "—"),
                new KeyValuePair<string, string>("Район", obj.Raion ?? "—"),
                new KeyValuePair<string, string>("Индекс", obj.Indexx ?? "—"),
                new KeyValuePair<string, string>("Кадастровый номер", obj.CadastralNumber ?? "—"),
                new KeyValuePair<string, string>("Год постройки", obj.ConstructionYear ?? "—"),
                new KeyValuePair<string, string>("Этажность", obj.Levels ?? "—"),
                new KeyValuePair<string, string>("Дата ввода в эксплуатацию", obj.CommissioningDate ?? "—"),
                new KeyValuePair<string, string>("Площадь зданий", obj.SqureBuildings ?? "—"),
                new KeyValuePair<string, string>("Площадь территорий", obj.SqureTerritory ?? "—"),
                new KeyValuePair<string, string>("Используемая территория", obj.TerritoriesUsed ?? "—"),
                new KeyValuePair<string, string>("Зелёные насаждения", obj.GreenSpaces ?? "—")
            };

            GridView1.DataSource = ToDataTableTransposed(items);
            GridView1.DataBind();
        }

        // =====================================================================
        // СКАЧИВАНИЕ: видимые данные
        // =====================================================================

        protected void Vidim_Click(object sender, EventArgs e)
        {
            BuildFullReport();

            foreach (ListItem check in Chk_view.Items)
            {
                if (!check.Selected) continue;

                switch (check.Text)
                {
                    case "Основная информация": pokaz_table_diapazon(GridView1, 0, 11); break;
                    case "Сведения о зданиях/сооружениях": pokaz_table_diapazon(GridView1, 12, 24); break;
                    case "Сведения о земельном участке": pokaz_table_diapazon(GridView1, 12, 24); break;
                    case "Инженерные системы": pokaz_table_diapazon(GridView1, 25, 48); break;
                    case "Документы": pokaz_table_diapazon(GridView1, 49, 82); break;
                    case "Иная документация": pokaz_table_diapazon(GridView1, 83, 104); break;
                    case "Прочие данные": pokaz_table_diapazon(GridView1, 105, 110); break;
                    case "Спортивные зоны": pokaz_table_diapazon(GridView1, 111, 111); break;
                }
            }

            download(GridView1, "Отчёт_по_" + Label1.Text);
            skrit_table(GridView1);
        }

        // =====================================================================
        // СКАЧИВАНИЕ КАЖДОЙ ТАБЛИЦЫ
        // =====================================================================

        protected void btn_dow_inf_Click(object sender, ImageClickEventArgs e) => download(Table_inf, Osnov_inf.Text + "_по_" + Label1.Text);
        protected void btn_dow_cat_Click(object sender, ImageClickEventArgs e) => download(Table_kat, Kategor.Text + "_по_" + Label1.Text);
        protected void btn_dow_sved_Click(object sender, ImageClickEventArgs e) => download(Table_sved, Svedenia.Text + "_по_" + Label1.Text);
        protected void btn_dow_ing_Click(object sender, ImageClickEventArgs e) => download(Table_ing, Ing_sys.Text + "_по_" + Label1.Text);
        protected void btn_dow_in_Click(object sender, ImageClickEventArgs e) => download(Table_in, In_doc.Text + "_по_" + Label1.Text);
        protected void btn_dow_proch_Click(object sender, ImageClickEventArgs e) => download(Table_proch, Proch_doc.Text + "_по_" + Label1.Text);
        protected void btn_dow_tabl_Click(object sender, ImageClickEventArgs e) => download(Tabl_tabl, label_tabl.Text + "_по_" + Label1.Text);
        protected void btn_dow_zone_Click(object sender, ImageClickEventArgs e) => download(Table_zone, Sports_zone.Text + "_по_" + Label1.Text);

        protected void btn_dow_tex_Click(object sender, ImageClickEventArgs e)
        {
            GridView_documents.Visible = true;
            download(GridView_documents, Tex_doc.Text + "_по_" + Label1.Text);
            GridView_documents.Visible = false;
        }

        // =====================================================================
        // МЕТОД СКАЧИВАНИЯ (общий)
        // =====================================================================

        public override void VerifyRenderingInServerForm(Control control)
        {
            // Нужно для RenderControl вне формы
        }

        /// <summary>
        /// Выгружает GridView в XLS-файл (HTML-таблица с расширением .xls).
        /// </summary>
        public void download(GridView grid, string otch_name)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + otch_name + ".xls");
            Response.ContentType = "application/vnd.ms-excel";
            Response.Charset = "utf-8";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1251");

            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                grid.AllowPaging = false;

                foreach (GridViewRow row in grid.Rows)
                {
                    if (row.Cells.Count > 1)
                    {
                        row.Cells[1].Width = 150;
                        row.Cells[0].Width = 200;
                    }
                    foreach (TableCell cell in row.Cells)
                    {
                        cell.HorizontalAlign = HorizontalAlign.Center;
                        cell.VerticalAlign = VerticalAlign.Middle;
                        cell.CssClass = "textmode";
                    }
                }

                grid.RenderControl(hw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }

        // =====================================================================
        // RowCreated / RowDataBound (заглушки для дизайнера)
        // =====================================================================

        protected void Table_inf_RowCreated(object sender, GridViewRowEventArgs e) { shirina(Table_inf); e.Row.Visible = false; }
        protected void Table_kat_RowCreated(object sender, GridViewRowEventArgs e) { shirina(Table_kat); e.Row.Visible = false; }
        protected void Table_sved_RowCreated(object sender, GridViewRowEventArgs e) { shirina(Table_sved); e.Row.Visible = false; }
        protected void Table_ing_RowCreated(object sender, GridViewRowEventArgs e) { shirina(Table_ing); e.Row.Visible = false; }
        protected void Table_in_RowCreated(object sender, GridViewRowEventArgs e) { shirina(Table_in); e.Row.Visible = false; }
        protected void Table_proch_RowCreated(object sender, GridViewRowEventArgs e) { shirina(Table_proch); e.Row.Visible = false; }
        protected void Table_tabl_RowCreated(object sender, GridViewRowEventArgs e) { shirina(Tabl_tabl); e.Row.Visible = false; }
        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e) { e.Row.Visible = false; }

        protected void Table_tex_RowCreated(object sender, GridViewRowEventArgs e)
        {
            var grid = sender as GridView;
            if (grid != null) shirina(grid);
            e.Row.Visible = false;
        }

        protected void Table_zone_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Visible = false;
            foreach (TableCell cell in e.Row.Cells)
                cell.Style.Add("padding", "5px");
        }

        protected void Table_zone_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells.Count > 12
                    && e.Row.Cells[12].Text != "" && e.Row.Cells[12].Text != "&nbsp;")
                {
                    e.Row.Cells[0].Attributes["onmouseover"] = "javascript:SetMouseOver(this)";
                    e.Row.Cells[0].Attributes["onmouseout"] = "javascript:SetMouseOut(this)";
                    e.Row.Cells[0].Attributes["onclick"] = "javascript:openImageWindow('" + e.Row.Cells[12].Text + "')";
                }
                if (Table_zone.HeaderRow != null && Table_zone.HeaderRow.Cells.Count > 0)
                    Table_zone.HeaderRow.Cells[0].Width = 200;
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    string data = Server.HtmlDecode(e.Row.Cells[i].Text);
                    if ((data.Length > 1) || (data.Length == 1 && data[0] >= '0' && data[0] <= '9'))
                        e.Row.Cells[i].Text = data;
                }
            }
            btn_tabl_tabl.Visible = false;
        }

        protected void Table_inf_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;

            for (int i = 1; i < e.Row.Cells.Count; i++)
            {
                if (e.Row.Cells[i].Text != "&nbsp;" && e.Row.Cells[i].Text != "")
                {
                    e.Row.Visible = true;
                    e.Row.Cells[0].Visible = true;
                    e.Row.Cells[1].Visible = true;
                }
                else
                {
                    e.Row.Visible = false;
                }
            }
        }

        protected void Dow_present_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells.Count > 1)
                e.Row.Cells[1].Visible = false;
        }

        protected void Object_image_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[0].Height = 160;
        }

        protected void Object_image_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "javascript:SetMouseOver(this)";
                e.Row.Attributes["onmouseout"] = "javascript:SetMouseOut(this)";
                e.Row.Attributes["onclick"] = "javascript:openImageWindow('" + e.Row.Cells[1].Text + "')";
            }
        }
    }
}