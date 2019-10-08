using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : Page
{
    private ComicsStoreBack.Service1Client client;
    private int AuthorSelectedIndex = 0;

    public _Default() : base()
    {
        client = new ComicsStoreBack.Service1Client("BasicHttpBinding_IService1");
    }

    public void AuthorChange(Object sender, EventArgs e)
    {
        var value = client.GetAuthorById(Convert.ToInt32(AuthorsNames.SelectedItem.Value));
        AuhtorInfo.Text = $"Имя {value.Name} Дата рождения {value.DateOfBirth}" ;

        var resComics = client.GetAuthorComics(Convert.ToInt32(AuthorsNames.SelectedItem.Value));
        ComicsNames.DataSource = CreateDataSourceComics(resComics);
        ComicsNames.DataBind();

        var valueComics = client.GetComicsById(Convert.ToInt32(ComicsNames.SelectedItem.Value));
        ComicsInfo.Text = $"Описание: {valueComics.Description}";
        ComicsPrice.Text = $"Цена: {valueComics.Price}";
        var resComicsAuthors = client.GetComicsAuthors(Convert.ToInt32(ComicsNames.SelectedItem.Value));
        var allAuthors = string.Empty;
        foreach (var res in resComicsAuthors)
        {
            allAuthors += " " + res.Name;
        }
        ComicsAuthors.Text = allAuthors;
    }

    public void ComicsChange(Object sender, EventArgs e)
    {
        var value = client.GetComicsById(Convert.ToInt32(ComicsNames.SelectedItem.Value));
        ComicsInfo.Text = $"Описание: {value.Description}";
        ComicsPrice.Text = $"Цена: {value.Price}";
        var resComicsAuthors = client.GetComicsAuthors(Convert.ToInt32(ComicsNames.SelectedItem.Value));
        var allAuthors = string.Empty;
        foreach (var res in resComicsAuthors)
        {
            allAuthors += " " + res.Name;
        }
        ComicsAuthors.Text = allAuthors;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var res = client.GetAllAuthors();
            AuthorsNames.DataSource = CreateDataSource(res);
            AuthorsNames.DataTextField = "TextField";
            AuthorsNames.DataValueField = "ValueField";
            AuthorsNames.DataBind();

            var resComics = client.GetAuthorComics(Convert.ToInt32(AuthorsNames.SelectedItem.Value));
            ComicsNames.DataSource = CreateDataSourceComics(resComics);
            ComicsNames.DataTextField = "TextField";
            ComicsNames.DataValueField = "ValueField";
            ComicsNames.DataBind();
        }
    }

    protected ICollection CreateDataSourceComics(ComicsStoreBack.Comics[] comics)
    {
        DataTable dt = new DataTable();

        dt.Columns.Add(new DataColumn("TextField", typeof(String)));
        dt.Columns.Add(new DataColumn("ValueField", typeof(int)));

        foreach (var result in comics)
        {
            dt.Rows.Add(CreateRow(result.Name, result.Id, dt));
        }
        DataView dv = new DataView(dt);
        return dv;
    }

    protected ICollection CreateDataSource(ComicsStoreBack.Author[] auhtors)
    {
        DataTable dt = new DataTable();
        
        dt.Columns.Add(new DataColumn("TextField", typeof(String)));
        dt.Columns.Add(new DataColumn("ValueField", typeof(int)));
        
        foreach (var result in auhtors)
        {
            dt.Rows.Add(CreateRow(result.Name, result.Id, dt));
        }
        DataView dv = new DataView(dt);
        return dv;
    }

    protected DataRow CreateRow(String Text, int Value, DataTable dt)
    {
        DataRow dr = dt.NewRow();
        
        dr[0] = Text;
        dr[1] = Value;

        return dr;
    }

    protected void deleteComics(object sender, EventArgs e)
    {
        client.removeComics(Convert.ToInt32(ComicsNames.SelectedItem.Value));
        var resComics = client.GetAuthorComics(Convert.ToInt32(AuthorsNames.SelectedItem.Value));
        ComicsNames.DataSource = CreateDataSourceComics(resComics);
        ComicsNames.DataTextField = "TextField";
        ComicsNames.DataValueField = "ValueField";
        ComicsNames.DataBind();
        var valueComics = client.GetComicsById(Convert.ToInt32(ComicsNames.SelectedItem.Value));
        ComicsInfo.Text = $"Описание: {valueComics.Description}";
        ComicsPrice.Text = $"Цена: {valueComics.Price}";
        var resComicsAuthors = client.GetComicsAuthors(Convert.ToInt32(ComicsNames.SelectedItem.Value));
        var allAuthors = string.Empty;
        foreach (var res in resComicsAuthors)
        {
            allAuthors += " " + res.Name;
        }
        ComicsAuthors.Text = allAuthors;
    }

    protected void increasePrice(object sender, EventArgs e)
    {
        client.increaseComicsPriceOfAuthor(Convert.ToInt32(newPrice.Text) ,Convert.ToInt32(AuthorsNames.SelectedItem.Value));
        
        var valueComics = client.GetComicsById(Convert.ToInt32(ComicsNames.SelectedItem.Value));
        ComicsPrice.Text = $"Цена: {valueComics.Price}";
    }
}