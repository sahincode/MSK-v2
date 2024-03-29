﻿namespace MSK.UI.ViewModels
{
    public class PaginatedList<T> : List<T> where T : class, new()
    {
        public PaginatedList()
        {

        }
        public PaginatedList(List<T> datas, int pageSize, int dataCount, int page, string query)
        {
            CurrentPage = page >= 0 ? page : 1;
            PageCount = (int)Math.Ceiling(dataCount / (double)pageSize);
            this.AddRange(datas);
            Query = query;
        }
        public int PageCount { get; set; }
        public int CurrentPage { get; set; }
        public bool HasNext { get => PageCount > CurrentPage; set { } }
        public bool HasPrev { get => CurrentPage != 1; set { } }
        public string ? Query { get; set; }
       

        public static PaginatedList<T> Create(IQueryable<T> query, int page, int pageSize ,string ? q=null)
        {
            page = page > 0 ? page : 1;


            return new PaginatedList<T>(query.Skip((page - 1) * pageSize).Take(pageSize).ToList(), pageSize, query.ToList().Count, page ,q);
        }
    }
}

