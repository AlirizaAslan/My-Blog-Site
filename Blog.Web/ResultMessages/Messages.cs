namespace Blog.Web.ResultMessages
{
    public static class Messages
    {
        public static class Article
        {//static class olduğu içi new lemeye gerek yok
            public static string Add(string articleTitle)
            {
                return $"{articleTitle} başlıklı makale başarıyla eklenmiştir.";
            }

            public static string Update(string articleTitle)
            {
                return $"{articleTitle} başlıklı makale başarıyla güncellenmiştir.";
            }

            public static string Delete(string articleTitle)
            {
                return $"{articleTitle} başlıklı makale başarıyla Silinmiştir.";
            }

            public static string UndoDelete(string articleTitle)
            {
                return $"{articleTitle} başlıklı makale başarıyla geri alınmıştır.";
            }
        }


        public static class Category
        {//static class olduğu içi new lemeye gerek yok
            public static string Add(string categoryName)
            {
                return $"{categoryName} başlıklı kategori başarıyla eklenmiştir.";
            }

            public static string Update(string categoryName)
            {
                return $"{categoryName} başlıklı kategori başarıyla güncellenmiştir.";
            }

            public static string Delete(string categoryName)
            {
                return $"{categoryName} başlıklı kategori başarıyla Silinmiştir.";
            }

            public static string UndoDelete(string categoryName)
            {
                return $"{categoryName} başlıklı kategori başarıyla geri alınmıştır.";
            }
        }


        public static class User
        {//static class olduğu içi new lemeye gerek yok
            public static string Add(string userName)
            {
                return $"{userName} email adresli kullanıcı başarıyla eklenmiştir.";
            }

            public static string Update(string userName)
            {
                return $"{userName} email adresli kullanıcı başarıyla güncellenmiştir.";
            }

            public static string Delete(string userName)
            {
                return $"{userName} email adresli kullanıcı başarıyla Silinmiştir.";
            }
        }
    }
}
