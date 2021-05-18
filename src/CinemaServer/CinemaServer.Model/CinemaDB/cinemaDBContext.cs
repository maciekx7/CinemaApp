using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CinemaServer.DBConnector.DBInfo;

#nullable disable

namespace CinemaServer.Model.cinemadb
{
    public partial class cinemadbContext : IdentityDbContext<Client>
    {
        public cinemadbContext()
        {
        }

        public cinemadbContext(DbContextOptions<cinemadbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<ReservationType> ReservationTypes { get; set; }
        public virtual DbSet<Screening> Screenings { get; set; }
        public virtual DbSet<Seat> Seats { get; set; }
        public virtual DbSet<SeatReserved> SeatReserveds { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                DBInfoData db = DBInfoData.GetInstance();
                optionsBuilder.UseMySQL($"server={db.Server};database={db.DatabaseName};user={db.UserName};password={db.Password};");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("cinemadb");
            modelBuilder.Entity<Client>(entity => entity.Property(m => m.Id).HasMaxLength(85));
            modelBuilder.Entity<Client>(entity => entity.Property(m => m.NormalizedEmail).HasMaxLength(85));
            modelBuilder.Entity<Client>(entity => entity.Property(m => m.NormalizedUserName).HasMaxLength(85));

            modelBuilder.Entity<IdentityRole>(entity => entity.Property(m => m.Id).HasMaxLength(85));
            modelBuilder.Entity<IdentityRole>(entity => entity.Property(m => m.NormalizedName).HasMaxLength(85));

            modelBuilder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(85));
            modelBuilder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.ProviderKey).HasMaxLength(85));
            modelBuilder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(85));
            modelBuilder.Entity<IdentityUserRole<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(85));

            modelBuilder.Entity<IdentityUserRole<string>>(entity => entity.Property(m => m.RoleId).HasMaxLength(85));

            modelBuilder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(85));
            modelBuilder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(85));
            modelBuilder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.Name).HasMaxLength(85));

            modelBuilder.Entity<IdentityUserClaim<string>>(entity => entity.Property(m => m.Id).HasMaxLength(85));
            modelBuilder.Entity<IdentityUserClaim<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(85));
            modelBuilder.Entity<IdentityRoleClaim<string>>(entity => entity.Property(m => m.Id).HasMaxLength(85));
            modelBuilder.Entity<IdentityRoleClaim<string>>(entity => entity.Property(m => m.RoleId).HasMaxLength(85));

            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("client", "cinemadb");

                entity.HasIndex(e => e.Id, "client_id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "email_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("email");

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("lastname");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("name");

                /*
                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password");
                */

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("phone");
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.ToTable("movie", "cinemadb");

                entity.HasIndex(e => e.Id, "movie_id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description");

                entity.Property(e => e.Dubbing).HasColumnName("dubbing");

                entity.Property(e => e.Duration).HasColumnName("duration");

                entity.Property(e => e.ImageName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("image_name");

                entity.Property(e => e.Producer)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("producer");

                entity.Property(e => e.Subtitles).HasColumnName("subtitles");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<ReservationType>(entity =>
            {
                entity.ToTable("reservation_type", "cinemadb");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code)
                    .HasMaxLength(45)
                    .HasColumnName("code");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description");

                entity.Property(e => e.Discount).HasColumnName("discount");
            });

            modelBuilder.Entity<Screening>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.MovieId })
                    .HasName("PRIMARY");

                entity.ToTable("screening", "cinemadb");

                entity.HasIndex(e => e.MovieId, "fk_Screening_Movie1_idx");

                entity.HasIndex(e => e.Id, "screening_id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.MovieId).HasColumnName("movie_id");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Time).HasColumnName("time");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.Screenings)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("movie_id_screening_fk");
            });

            modelBuilder.Entity<Seat>(entity =>
            {
                entity.ToTable("seat", "cinemadb");

                entity.HasIndex(e => e.Id, "id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.SeatColumn).HasColumnName("seat_column");

                entity.Property(e => e.SeatRow)
                    .IsRequired()
                    .HasMaxLength(2)
                    .HasColumnName("seat_row");
            });

            modelBuilder.Entity<SeatReserved>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.TicketId, e.ScreeningId, e.SeatId })
                    .HasName("PRIMARY");

                entity.ToTable("seat_reserved", "cinemadb");

                entity.HasIndex(e => e.ScreeningId, "screening_id_fk_idx");

                entity.HasIndex(e => e.SeatId, "seat_id_fk_idx");

                entity.HasIndex(e => e.TicketId, "ticket_id_fk_idx");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.TicketId).HasColumnName("ticket_id");

                entity.Property(e => e.ScreeningId).HasColumnName("screening_id");

                entity.Property(e => e.SeatId).HasColumnName("seat_id");

                entity.HasOne(d => d.Screening)
                    .WithMany(p => p.SeatReserveds)
                    .HasPrincipalKey(p => p.Id)
                    .HasForeignKey(d => d.ScreeningId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("screening_id_seatReservation_fk");

                entity.HasOne(d => d.Seat)
                    .WithMany(p => p.SeatReserveds)
                    .HasForeignKey(d => d.SeatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("seat_id_seatReservation_fk");

                entity.HasOne(d => d.Ticket)
                    .WithMany(p => p.SeatReserveds)
                    .HasPrincipalKey(p => p.Id)
                    .HasForeignKey(d => d.TicketId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ticket_id_seatReservation_fk");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.ScreeningId, e.ReservationTypeId, e.ClientId })
                    .HasName("PRIMARY");

                entity.ToTable("ticket", "cinemadb");

                entity.HasIndex(e => e.ClientId, "fk_Ticket_Client1_idx");

                entity.HasIndex(e => e.QrCode, "qr_code_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.ReservationTypeId, "reservation_type_id_fk_idx");

                entity.HasIndex(e => e.ScreeningId, "screening_id_idx");

                entity.HasIndex(e => e.Id, "ticket_id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.ScreeningId).HasColumnName("screening_id");

                entity.Property(e => e.ReservationTypeId).HasColumnName("reservation_type_id");

                entity.Property(e => e.ClientId).HasColumnName("Client_id");

                entity.Property(e => e.Price)
                    .IsRequired()
                    .HasMaxLength(8)
                    .HasColumnName("price");

                entity.Property(e => e.QrCode)
                    .HasMaxLength(36)
                    .HasColumnName("qr_code");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Ticket_Client1");

                entity.HasOne(d => d.ReservationType)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.ReservationTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("reservationType_ticket_id_fk");

                entity.HasOne(d => d.Screening)
                    .WithMany(p => p.Tickets)
                    .HasPrincipalKey(p => p.Id)
                    .HasForeignKey(d => d.ScreeningId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("screening_id_ticket_fk");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
