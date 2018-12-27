using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Data.Models.Core
{
    [Serializable]
    [DataContract]
    public class Subject : ISubject
    {
        [DataMember]
        public Guid Id { get; set; }

        [Display(Name = "Наименование")]
        [DataMember]
        public string Name { get; set; }

        public ICollection<Link> Links { get; set; }
        public ICollection<HistoryEntry> Histories { get; set; }

        public Subject()
        {
            Links = new List<Link>();
            Histories = new List<HistoryEntry>();
        }

        public override string ToString()
        {
            return Name;
        }

        public string Serialize()
        {

            using (MemoryStream memoryStream = new MemoryStream())
            using (StreamReader reader = new StreamReader(memoryStream))
            {
                DataContractSerializer serializer = new DataContractSerializer(this.GetType());
                serializer.WriteObject(memoryStream, this);
                memoryStream.Position = 0;

                return reader.ReadToEnd();
            }
        }

        public static Subject Deserialize(string valueXML, Type toType)
        {
            using (Stream stream = new MemoryStream())
            {
                try
                {
                    byte[] data = Encoding.UTF8.GetBytes(valueXML);
                    stream.Write(data, 0, data.Length);
                    stream.Position = 0;
                    DataContractSerializer deserializer = new DataContractSerializer(toType);

                    return deserializer.ReadObject(stream) as Subject;
                }
                catch(Exception)
                {
                    return null;
                }

            }
        }
    }

    class SubjectConfig : EntityTypeConfiguration<Subject>
    {
        public SubjectConfig()
        {
            Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute() { IsUnique = false }));

            HasMany(p => p.Links).WithRequired(p => p.Subject);
        }
    }

}
