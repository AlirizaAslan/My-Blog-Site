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
    }
}
