using ComicsStoreBack.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ComicsStoreBack
{
    public class DatabaseHelper
    {
        private SqlConnectionStringBuilder connStringBuilder;
        private SqlConnection conn;

        public DatabaseHelper()
        {
            connStringBuilder = new SqlConnectionStringBuilder();
            connStringBuilder.DataSource = "DESKTOP-5OO9UBH\\SQLEXPRESS";
            connStringBuilder.InitialCatalog = "ComicsStore";
            connStringBuilder.TrustServerCertificate = true;
            connStringBuilder.Authentication = SqlAuthenticationMethod.SqlPassword;
            connStringBuilder.Password = "admin";
            connStringBuilder.UserID = "ComicsStoreAdmin";

            conn = new SqlConnection(connStringBuilder.ToString());
        }

        public List<Author> SelectAllAuthors()
        {
            try
            {
                List<Author> Authors = new List<Author>();
                SqlCommand com = conn.CreateCommand();
                com.CommandText = "Select * from Author";
                com.CommandType = CommandType.Text;
                conn.Open();
                SqlDataReader reader = com.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Authors.Add(new Author
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            DateOfBirth = reader.GetInt32(2)
                        });
                    }
                }
                reader.Close();

                return Authors;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public List<Comics> SelectAllComics()
        {
            try
            {
                List<Comics> Comics = new List<Comics>();
                SqlCommand com = conn.CreateCommand();
                com.CommandText = "Select * from Comics";
                com.CommandType = CommandType.Text;
                conn.Open();
                SqlDataReader reader = com.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Comics.Add(new Comics
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                            Year = reader.GetInt32(3),
                            Price = reader.GetInt32(4),
                            Publisher = reader.GetString(5)
                        });
                    }
                }
                reader.Close();

                return Comics;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public Author SelectAuthorById(int id)
        {
            try
            {
                Author Author = new Author();
                SqlCommand com = conn.CreateCommand();
                com.CommandText = "Select * from Author where Id = @id";
                com.Parameters.AddWithValue("@id", id);
                com.CommandType = CommandType.Text;
                conn.Open();
                SqlDataReader reader = com.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Author = new Author
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            DateOfBirth = reader.GetInt32(3),
                        };
                    }
                }
                reader.Close();

                return Author;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public Comics SelectComicsById(int id)
        {
            try
            {
                Comics comics = new Comics();
                SqlCommand com = conn.CreateCommand();
                com.CommandText = "Select * from Comics where Id = @id";
                com.Parameters.AddWithValue("@id", id);
                com.CommandType = CommandType.Text;
                conn.Open();
                SqlDataReader reader = com.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        comics = new Comics
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                            Year = reader.GetInt32(3),
                            Price = reader.GetInt32(4),
                            Publisher = reader.GetString(5)
                        };
                    }
                }
                reader.Close();

                return comics;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public List<Comics> SelectAuthorComics(int authorId)
        {
            try
            {
                List<Comics> comics = new List<Comics>();
                SqlCommand com = conn.CreateCommand();
                com.CommandText = "Select * from Comics where Id in (Select ComicsId from ComicsOfAuthor where AuthorId = @authorId)";
                com.Parameters.AddWithValue("@authorId", authorId);
                com.CommandType = CommandType.Text;
                conn.Open();
                SqlDataReader reader = com.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        comics.Add(new Comics
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                            Year = reader.GetInt32(3),
                            Price = reader.GetInt32(4),
                            Publisher = reader.GetString(5)
                        });
                    }
                }
                reader.Close();

                return comics;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public List<Author> SelectComicsAuthor(int comicsId)
        {
            try
            {
                List<Author> author = new List<Author>();
                SqlCommand com = conn.CreateCommand();
                com.CommandText = "Select * from Author where Id in (Select AuthorId from ComicsOfAuthor where ComicsId = @comicsId)";
                com.Parameters.AddWithValue("@comicsId", comicsId);
                com.CommandType = CommandType.Text;
                conn.Open();
                SqlDataReader reader = com.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        author.Add(new Author
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            DateOfBirth = reader.GetInt32(2)
                        });
                    }
                }
                reader.Close();

                return author;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
        
        public bool IncreaseComicsPriceOfAuthor(int newPrice, int authorId)
        {
            try
            {
                SqlCommand com = conn.CreateCommand();
                com.CommandText = "Update Comics Set Price = @newPrice Where Id in (Select ComicsId from ComicsOfAuthor where AuthorId = @authorId)";
                com.Parameters.AddWithValue("@authorId", authorId);
                com.Parameters.AddWithValue("@newPrice", newPrice);
                com.CommandType = CommandType.Text;
                conn.Open();

                return com.ExecuteNonQuery() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public bool ChangeAuthorNameOfComics(int comicsId, string newName)
        {
            try
            {
                SqlCommand com = conn.CreateCommand();
                com.CommandText = "Update Author Set Name = @newName Where Id in (Select AuthorId from ComicsOfAuthor where ComicsId = @comicsId)";
                com.Parameters.AddWithValue("@comicsId", comicsId);
                com.Parameters.AddWithValue("@newName", newName);
                com.CommandType = CommandType.Text;
                conn.Open();

                return com.ExecuteNonQuery() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public bool DeleteComics(int comicsId)
        {
            try
            {
                SqlCommand com = conn.CreateCommand();
                com.CommandText = "Delete from ComicsOfAuthor where ComicsId = @comicsId; Delete from Comics where Id = @comicsId";
                com.Parameters.AddWithValue("@comicsId", comicsId);

                com.CommandType = CommandType.Text;
                conn.Open();

                return com.ExecuteNonQuery() > 0;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

    }
}