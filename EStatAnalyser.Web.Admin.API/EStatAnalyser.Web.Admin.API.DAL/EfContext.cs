using EStatAnalyser.Web.Admin.API.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace EStatAnalyser.Web.Admin.API.DAL
{
    public class EfContext : DbContext
    {
        // Имена свойств-database сетов отражают имена будущих таблиц
        public DbSet<Data> DATAS { get; set; }
        public DbSet<Value> VALUES { get; set; }

        // Загрузка конфигурации происходит извне
        public EfContext(DbContextOptions options) : base(options) {}

        // Содержит такие элементы модели БД как ключи и связи
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Первичные ключи
            modelBuilder.Entity<Data>().HasKey(x => x.Id);
            modelBuilder.Entity<Value>().HasKey(x =>x.Id);

            // Описание связи один-ко-многим, принятой в проекте + логика удаления данных
            // Если запись в таблице DATAS будет удалена, то все ссылающиеся записи из VALUES каскадно будут удалены
            modelBuilder.Entity<Value>()
                .HasOne(x => x.CurrentData)
                .WithMany(x => x.Values)
                .HasForeignKey(x => x.DataId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}