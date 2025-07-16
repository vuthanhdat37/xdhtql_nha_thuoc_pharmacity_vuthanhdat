using Microsoft.EntityFrameworkCore;

namespace web_quanlynhathuoc.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Thuoc> Thuocs { get; set; }
        public DbSet<LoaiThuoc> LoaiThuocs { get; set; }
        public DbSet<CuaHang> CuaHangs { get; set; }
        public DbSet<CTTC> CTTCs { get; set; }
        public DbSet<TieuChuan> TieuChuans { get; set; }
        public DbSet<NhanVien> NhanViens { get; set; }
        public DbSet<NhaCungCap> NhaCungCaps { get; set; }
        public DbSet<KhachHang> KhachHangs { get; set; }


        // Thêm mới các bảng liên quan đến thống kê - báo cáo
        public DbSet<HoaDon> HoaDons { get; set; }
        public DbSet<CTHD> CTHDs { get; set; }
        public DbSet<PhieuMuaHang> PhieuMuaHangs { get; set; }
        public DbSet<CTPMH> CTPMHs { get; set; }
        public DbSet<DonDatHang> DonDatHangs { get; set; }
        public DbSet<CTDDH> CTDDHs { get; set; }
        public DbSet<PhieuGiaoHang> PhieuGiaoHangs { get; set; }
        public DbSet<CTPGH> CTPGHs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình khóa chính kép cho các bảng liên kết
            modelBuilder.Entity<CTTC>()
                .HasKey(c => new { c.MaTC_ID, c.MaCH_ID });

            modelBuilder.Entity<CTPMH>()
                .HasKey(c => new { c.MaPMH_ID, c.MaThuoc_ID });

            modelBuilder.Entity<CTHD>()
                .HasKey(c => new { c.MaHD_ID, c.MaThuoc_ID });

            modelBuilder.Entity<CTDDH>()
                .HasKey(c => new { c.MaDDH_ID, c.MaThuoc_ID });

            modelBuilder.Entity<CTPGH>()
                .HasKey(c => new { c.MaPGH_ID, c.MaThuoc_ID });
        }
    }
}
