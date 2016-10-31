namespace Databas_inlämningsupg1
{
    using System;
    using System.Data.Entity;
    using System.Linq;
/*    Därefter bygger du en Windows Forms-applikation som hanterar kontakter.Du ska använda Entity
Framework Code First och låta C#-koden driva fram databasstrukturen.
Kontaktdatabasen ska lagra -------namn, gatudress, postnummer, postort, telefon, e-post och födelsedag-------- för
varje kontakt. Dessutom ska varje post ha en nyckel.
Man ska kunna lägga till nya, ändra befintliga och ta bort poster ur kontaktdatabasen.
Det skall gå att söka på namn och sökresultatet ska visas i en lista. Från listan skall det gå att klicka på
kontakten och därefter se alla detaljer om kontakten.*/
    public class InUpg1 : DbContext
    {
        public DbSet<Personer> Pers { get; set; }
    }

    public class Personer
    {
        public int PersonerId { get; set; }
        public string Namn { get; set; }
        public string GatuAdress { get; set; }
        public string  PostNummer { get; set; }
        public string  Postort { get; set; }
        public string Telefon { get; set; }
        public string Epost { get; set; }
        public DateTime Födelsedag { get; set; }
    }
}