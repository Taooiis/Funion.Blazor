namespace FunionBlazor.Web.Core.Data;
public class DataPage<T>
{
    public IQueryable<T> Datas { get; set; }
    public int PageIndex { get; set; } = 1;

    public int PageSize { get; set; } = 10;

    public int PageCount => (int)Math.Ceiling(CurrentCount / (double)PageSize);

    public int CurrentCount => GetFilterDatas().Count();

    public DataPage(IQueryable<T> datas)
    {
        Datas = datas;
    }
    public virtual IEnumerable<T> GetFilterDatas()
    {
        IEnumerable<T> datas = Datas;
        return datas;
    }
    public virtual List<T1> GetFilterlistData<T1>(Expression<Func<T, T1>> selectExpression)
    {
        return Datas.Select(selectExpression).ToList();
    }
    public virtual List<T1> GetFilterlistData<T1>(Expression<Func<T, bool>> whereExpression, Expression<Func<T, T1>> selectExpression)
    {
        return Datas.Where(whereExpression).Select(selectExpression).ToList();
    }
    public virtual List<T> GetPageDatas()
    {
        if(GetFilterDatas().Count() < (PageIndex - 1) * PageSize) PageIndex = 1;
        return GetFilterDatas().Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
    }
}

