using System;
using System.Collections.Generic;
//using LearnAPI.Modal;
//using LearnAPI.Repos.Models;
using Microsoft.EntityFrameworkCore;
using HandT_Api_Layer.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

//namespace LearnAPI.Repos;
namespace HandT_Api_Layer.DomainInterface
{

    public partial class DestinationImageRepo : DbContext
    {
        public DestinationImageRepo()
        {
        }

        public DestinationImageRepo(DbContextOptions<DestinationImageRepo> options)
            : base(options)
        {
        }

        //public virtual DbSet<TblCustomer> TblCustomers { get; set; }

        //public virtual DbSet<Destination_details> TblProducts { get; set; }

        public virtual DbSet<DestinationImage> DestinationImages { get; set; }

        //public virtual DbSet<TblRefreshtoken> TblRefreshtokens { get; set; }

        //public virtual DbSet<TblUser> TblUsers { get; set; }

        //public virtual DbSet<Customermodal> customerdetail { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}