namespace EStatAnalyser.Web.Admin.API.Core.Entities
{
    public class Value
    {
        /// <summary>
        /// ID объекта, служит первичным ключом базы данных VALUES
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Значение Х
        /// </summary>
        public double X { get; set; }
        /// <summary>
        /// Значение Y
        /// </summary>
        public double Y { get; set; }
        /// <summary>
        /// Foreign key, ссылающийся соответствующий ему DATAS объект
        /// </summary>
        public int DataId { get; set; }
        /// <summary>
        /// Относится непосредственно к объекту C#, использоваться будет в программе только в Services и DAL, включён в структуру базы данных не будет.
        /// Модификатор virtual обозначает, что над значениями будет использован принцип LazyLoading [20]
        /// </summary>
        public virtual Data? CurrentData { get; set; }
    }
}