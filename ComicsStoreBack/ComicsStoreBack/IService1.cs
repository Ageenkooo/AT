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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        List<Author> GetAllAuthors();

        [OperationContract]
        List<Comics> GetAllComics();

        [OperationContract]
        Author GetAuthorById(int id);

        [OperationContract]
        Comics GetComicsById(int id);

        [OperationContract]
        List<Comics> GetAuthorComics(int authorId);

        [OperationContract]
        List<Author> GetComicsAuthors(int comicsId);

        [OperationContract]
        void increaseComicsPriceOfAuthor(int newPrice, int authorId);

        [OperationContract]
        void changeAuthorNameOfComics(int comicsId, string newName);

        [OperationContract]
        void removeComics(int id);
    }
    
}
