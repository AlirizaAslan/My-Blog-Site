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
        }
    }
}
