using ComicsStoreBack.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ComicsStoreBack
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        DatabaseHelper dbHelper = new DatabaseHelper();

        public List<Author> GetAllAuthors()
        {
            return dbHelper.SelectAllAuthors();
        }

        public List<Comics> GetAllComics()
        {
            return dbHelper.SelectAllComics();
        }


        public Author GetAuthorById(int id)
        {
            return dbHelper.SelectAuthorById(id);
        }


        public Comics GetComicsById(int id)
        {
            return dbHelper.SelectComicsById(id);
        }


        public List<Comics> GetAuthorComics(int authorId)
        {
            return dbHelper.SelectAuthorComics(authorId);
        }


        public List<Author> GetComicsAuthors(int comicsId)
        {
            return dbHelper.SelectComicsAuthor(comicsId);
        }


        public void increaseComicsPriceOfAuthor(int newPrice, int authorId)
        {
            dbHelper.IncreaseComicsPriceOfAuthor(newPrice, authorId);
        }

        public void changeAuthorNameOfComics(int comicsId, string newName)
        {
            dbHelper.ChangeAuthorNameOfComics(comicsId, newName);
        }


        public void removeComics(int id)
        {
            dbHelper.DeleteComics(id);
        }
    }
}
