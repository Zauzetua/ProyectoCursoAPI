namespace ProyectoCursoAPI.Web.Models.Shared
{
    // Modelo de paginacion para las vistas parciales de paginado
    public class PaginationInfo
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
        public string TargetAction { get; set; } = "GetTable";
    }
}
