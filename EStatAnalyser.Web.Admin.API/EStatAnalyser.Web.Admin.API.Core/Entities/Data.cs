namespace EStatAnalyser.Web.Admin.API.Core.Entities
{
    public class Data
    {
        /// <summary>
        /// ID объекта, служит первичным ключом базы данных DATAS
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Обозначение поля Х, например "σ, г/см3"
        /// </summary>
        public string? XFieldName { get; set; }
        /// <summary>
        /// Обозначение поля Y, например "К, м3/с"
        /// </summary>
        public string? YFieldName { get; set; }
        /// <summary>
        /// К какой области относятся переменные выборки, не более 3-5 слов
        /// </summary>
        public string? DataType { get; set; }
        /// <summary>
        /// Описание выборки
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// Логический вентиль, отвечающий на вопрос проводился ли анализ над выборкой
        /// </summary>
        public bool WasAnalysed { get; set; }
        /// <summary>
        /// Относится непосредственно к объекту C#, использоваться будет в программе только в Services и DAL, включён в структуру базы данных не будет.
        /// Модификатор virtual обозначает, что над значениями будет использован принцип LazyLoading [20]
        /// </summary>
        public virtual List<Value>? Values { get; set; }
    }
}