namespace Excel2Sqlite
{
    public class SelectSqlColumnsViewModel
    {
        public string Header { get; set; }
        public bool IsSelected { get; set; } = true;

        public SelectSqlColumnsViewModel(string header)
        {
            Header = header;
        }
    }
}
