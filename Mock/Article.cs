using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using localmock.Models;

namespace localmock.Mock
{
  public class ArticleMock
  {
    private static List<Article> _articles;

    static ArticleMock()
    {
      _articles = new List<Article>();
      for (int i = 0; i < 1000; i++)
      {
        _articles.Add(new Article()
        {
          importance = i % 5,
          author = $"author{i}",
          category_item = $"category_item{i}",
          comment_disabled = i % 2 == 0,
          content = $"content{i}",
          content_short = $"content_short{i}",
          display_time = DateTime.Now.ToString("yyyy MMMM dd HH:mm:ss"),
          id = i.ToString(),
          image_uri = $"image_uri{i}",
          platforms = $"platforms{i}",
          source_name = $"source_name{i}",
          source_uri = $"source_uri{i}",
          status = "published",
          tags = "tags",
          title = "title"+i
        });
      }
    }
    public static List<Article> List(int page, int limit, string title)
    {
      var query = _articles.AsQueryable();
      if (!string.IsNullOrEmpty(title))
      {
        query = query.Where(a => a.title.Contains(title));
      }
      return query.Skip((page - 1) * limit).Take(limit).ToList();
    }
    public static int Total()
    {
      return _articles.Count;
    }
  }

}
