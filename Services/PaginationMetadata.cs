using System;

namespace InfoCity.API.Services
{
    public class PaginationMetadata
    {
        public int totalItemCount { get; set; }
        public int totalPageCount { get; set; }
        public int pageSize { get; set; }
        public int CurrentPage { get; set; }

        public PaginationMetadata(int totalItemCount, int pageSize, int currentPage)
        {
            this.totalItemCount = totalItemCount;
            this.pageSize = pageSize;
            CurrentPage = currentPage;
            totalPageCount = (int) Math.Ceiling(totalItemCount / (double)pageSize);
        }
    }
}
