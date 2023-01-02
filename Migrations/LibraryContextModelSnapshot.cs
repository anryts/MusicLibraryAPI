﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MusicLibraryAPI.Data;

#nullable disable

namespace MusicLibraryAPI.Migrations
{
    [DbContext(typeof(LibraryContext))]
    partial class LibraryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MusicLibraryAPI.Entities.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_genres");

                    b.ToTable("genres", (string)null);
                });

            modelBuilder.Entity("MusicLibraryAPI.Entities.Song", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("GenreId")
                        .HasColumnType("int")
                        .HasColumnName("genre_id");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("title");

                    b.HasKey("Id")
                        .HasName("pk_songs");

                    b.HasIndex("GenreId")
                        .HasDatabaseName("ix_songs_genre_id");

                    b.ToTable("songs", (string)null);
                });

            modelBuilder.Entity("MusicLibraryAPI.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("last_name");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("MusicLibraryAPI.Entities.UserSong", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("SongId")
                        .HasColumnType("int")
                        .HasColumnName("song_id");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.Property<bool>("isFavourite")
                        .HasColumnType("bit")
                        .HasColumnName("is_favourite");

                    b.HasKey("Id")
                        .HasName("pk_user_song");

                    b.HasIndex("SongId")
                        .HasDatabaseName("ix_user_song_song_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_user_song_user_id");

                    b.ToTable("user_song", (string)null);
                });

            modelBuilder.Entity("MusicLibraryAPI.Entities.Song", b =>
                {
                    b.HasOne("MusicLibraryAPI.Entities.Genre", "Genre")
                        .WithMany()
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_songs_genres_genre_id");

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("MusicLibraryAPI.Entities.UserSong", b =>
                {
                    b.HasOne("MusicLibraryAPI.Entities.Song", "Song")
                        .WithMany()
                        .HasForeignKey("SongId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_song_songs_song_id");

                    b.HasOne("MusicLibraryAPI.Entities.User", "User")
                        .WithMany("UserSongs")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_song_users_user_id");

                    b.Navigation("Song");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MusicLibraryAPI.Entities.User", b =>
                {
                    b.Navigation("UserSongs");
                });
#pragma warning restore 612, 618
        }
    }
}
