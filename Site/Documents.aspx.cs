using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site
{
    public partial class Documents : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var panel = (System.Web.UI.WebControls.Panel)Master.FindControl("Panel5");
            if (panel != null) panel.BackColor = System.Drawing.Color.FromArgb(248, 145, 59);

            if (!IsPostBack)
            {
                LoadFilters();

                Documents_search.Text = (Session["Search_Master2"] as string) ?? "";
                BindDocuments();
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "documGrid", "docum_grid();", true);
        }

        // =====================================================================
        // ЗАГРУЗКА ФИЛЬТРОВ
        // =====================================================================

        private void LoadFilters()
        {
            var docs = LocalData.GetDocuments();

            DropDownList_doctype.DataSource = docs
                .Select(d => d.DocType).Where(v => !string.IsNullOrEmpty(v) && v != "Оригинал (2-й экземпляр)")
                .Distinct().OrderBy(v => v)
                .Select(v => new { DocType = v }).ToList();
            DropDownList_doctype.DataTextField = "DocType";
            DropDownList_doctype.DataValueField = "DocType";
            DropDownList_doctype.DataBind();

            DropDownList_storage_location.DataSource = docs
                .Select(d => d.StorageLocation).Where(v => !string.IsNullOrEmpty(v))
                .Distinct().OrderBy(v => v)
                .Select(v => new { StorageLocation = v }).ToList();
            DropDownList_storage_location.DataTextField = "StorageLocation";
            DropDownList_storage_location.DataValueField = "StorageLocation";
            DropDownList_storage_location.DataBind();

            DropDownList_responsible_person.DataSource = docs
                .Select(d => d.ResponsiblePerson).Where(v => !string.IsNullOrEmpty(v))
                .Distinct().OrderBy(v => v)
                .Select(v => new { ResponsiblePerson = v }).ToList();
            DropDownList_responsible_person.DataTextField = "ResponsiblePerson";
            DropDownList_responsible_person.DataValueField = "ResponsiblePerson";
            DropDownList_responsible_person.DataBind();

            DropDownList_responsible_department.DataSource = docs
                .Select(d => d.ResponsibleDepartment).Where(v => !string.IsNullOrEmpty(v))
                .Distinct().OrderBy(v => v)
                .Select(v => new { ResponsibleDepartment = v }).ToList();
            DropDownList_responsible_department.DataTextField = "ResponsibleDepartment";
            DropDownList_responsible_department.DataValueField = "ResponsibleDepartment";
            DropDownList_responsible_department.DataBind();

            DropDownList_department.DataSource = docs
                .Select(d => d.Department).Where(v => !string.IsNullOrEmpty(v))
                .Distinct().OrderBy(v => v)
                .Select(v => new { Department = v }).ToList();
            DropDownList_department.DataTextField = "Department";
            DropDownList_department.DataValueField = "Department";
            DropDownList_department.DataBind();
        }

        // =====================================================================
        // ПРИВЯЗКА ДАННЫХ
        // =====================================================================

        private void BindDocuments()
        {
            var query = LocalData.GetDocuments().AsEnumerable();

            // Поиск по тексту
            var search = Documents_search.Text.Trim();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(d =>
                    Contains(d.Doc, search) ||
                    Contains(d.DocType, search) ||
                    Contains(d.StorageLocation, search) ||
                    Contains(d.ResponsiblePerson, search) ||
                    Contains(d.ResponsibleDepartment, search) ||
                    Contains(d.Department, search) ||
                    Contains(d.RegulatoryDocumentIn, search) ||
                    Contains(d.RegulatoryDocumentEx, search) ||
                    Contains(d.Frequency, search));
            }

            // Фильтры по DropDownList
            if (DropDownList_doctype.SelectedValue != "%")
                query = query.Where(d => d.DocType == DropDownList_doctype.SelectedValue);
            if (DropDownList_storage_location.SelectedValue != "%")
                query = query.Where(d => d.StorageLocation == DropDownList_storage_location.SelectedValue);
            if (DropDownList_responsible_person.SelectedValue != "%")
                query = query.Where(d => d.ResponsiblePerson == DropDownList_responsible_person.SelectedValue);
            if (DropDownList_responsible_department.SelectedValue != "%")
                query = query.Where(d => d.ResponsibleDepartment == DropDownList_responsible_department.SelectedValue);
            if (DropDownList_department.SelectedValue != "%")
                query = query.Where(d => d.Department == DropDownList_department.SelectedValue);

            var list = query.ToList();

            GridView_documents.DataSource = list;
            GridView_documents.DataBind();
        }

        private static bool Contains(string source, string search) =>
            !string.IsNullOrEmpty(source) && source.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0;

        public void CorrectBind()
        {
            BindDocuments();
        }

        // =====================================================================
        // ОБРАБОТЧИКИ ФИЛЬТРОВ
        // =====================================================================

        protected void DropDownList_doctype_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDocuments();
        }

        protected void Documents_Button_reset_Click(object sender, EventArgs e)
        {
            DropDownList_doctype.SelectedValue = "%";
            DropDownList_storage_location.SelectedValue = "%";
            DropDownList_responsible_person.SelectedValue = "%";
            DropDownList_responsible_department.SelectedValue = "%";
            DropDownList_department.SelectedValue = "%";
            BindDocuments();
        }

        protected void Documents_search_TextChanged(object sender, EventArgs e)
        {
            Session["Search_Master2"] = Documents_search.Text;
            BindDocuments();

            // Применяем переключатели видимости
            ApplyVisibilityCheckboxes();
        }

        // =====================================================================
        // ЧЕКБОКСЫ ВИДИМОСТИ КОЛОНОК
        // =====================================================================

        protected void Documents_maincontent_block_chk_pers_CheckedChanged(object sender, EventArgs e)
        {
            if (Documents_maincontent_block_chk_pers.Checked)
            {
                Documents_maincontent_block_chk_rucv.Checked = false;
                Documents_all_elements.Checked = false;
                DropDownList_responsible_person.Visible = true;
                DropDownList_responsible_department.Visible = false;
                DropDownList_responsible_person.CssClass = "select select_2";
                SetColumnVisibility(showPerson: true, showDepartment: false);
            }
            else
            {
                ApplyVisibilityCheckboxes();
            }
        }

        protected void Documents_maincontent_block_chk_rucv_CheckedChanged(object sender, EventArgs e)
        {
            if (Documents_maincontent_block_chk_rucv.Checked)
            {
                Documents_maincontent_block_chk_pers.Checked = false;
                Documents_all_elements.Checked = false;
                DropDownList_responsible_person.Visible = false;
                DropDownList_responsible_department.Visible = true;
                DropDownList_responsible_department.CssClass = "select select_2";
                SetColumnVisibility(showPerson: false, showDepartment: true);
            }
            else
            {
                ApplyVisibilityCheckboxes();
            }
        }

        protected void Documents_all_elements_CheckedChanged(object sender, EventArgs e)
        {
            if (Documents_all_elements.Checked)
            {
                Documents_maincontent_block_chk_pers.Checked = false;
                Documents_maincontent_block_chk_rucv.Checked = false;
                DropDownList_responsible_person.Visible = true;
                DropDownList_responsible_department.Visible = true;
                SetColumnVisibility(showPerson: true, showDepartment: true);
            }
        }

        /// <summary>
        /// Показывает / скрывает 3-ю и 4-ю колонки вложенного GridView.
        /// </summary>
        private void SetColumnVisibility(bool showPerson, bool showDepartment)
        {
            foreach (GridViewRow row in GridView_documents.Rows)
            {
                var inner = (GridView)row.FindControl("GridView_inner_documents");
                if (inner == null) continue;

                foreach (GridViewRow innerRow in inner.Rows)
                {
                    if (innerRow.Cells.Count > 2) innerRow.Cells[2].Visible = showPerson;
                    if (innerRow.Cells.Count > 3) innerRow.Cells[3].Visible = showDepartment;
                }
            }
        }

        private void ApplyVisibilityCheckboxes()
        {
            bool showPerson = Documents_maincontent_block_chk_pers.Checked || Documents_all_elements.Checked;
            bool showDepartment = Documents_maincontent_block_chk_rucv.Checked || Documents_all_elements.Checked;
            SetColumnVisibility(showPerson, showDepartment);
        }

        // =====================================================================
        // ВЛОЖЕННЫЙ GRIDVIEW: скрытие пустых строк
        // =====================================================================

        protected void GridView_documents_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = "javascript:ClickDoc()";

                var inner = (GridView)e.Row.FindControl("GridView_inner_documents");
                if (inner != null)
                {
                    // Передаём запись в вложенный GridView — он получит ту же строку,
                    // что и родительский (это "детальная" информация по документу).
                    var dataItem = e.Row.DataItem as LocalData.DocumentRecord;
                    if (dataItem != null)
                    {
                        inner.DataSource = new[] { dataItem };
                        inner.DataBind();
                    }
                }

                foreach (TableCell cell in e.Row.Cells)
                {
                    if (cell.Text == "&nbsp;") cell.CssClass = "empty__row";
                }
            }
        }

        protected void GridView_inner_documents_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // По умолчанию показываем все колонки
                e.Row.Visible = true;
                foreach (TableCell cell in e.Row.Cells)
                {
                    if (cell.Text == "&nbsp;") cell.CssClass = "empty__row";
                }
            }
        }
    }
}