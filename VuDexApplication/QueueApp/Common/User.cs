using QueueApp.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace QueueApp.Common
{
    [Serializable]
    public class User
    {
        #region Fields

        private int userId;
        private String subscribedTo;    //name of queue
        private bool isListening = false;
        public virtual List<Item> Items { get; set; }
        public virtual List<Position> Positions { get; set; }
        
        //server to communicate with
        #endregion

        #region Properties
        [XmlAttribute("subscribedTo")]
        public String SubscribedTo { get { return subscribedTo; } set { subscribedTo = value; } }

        [XmlAttribute("id")]
        public int UserId
        {
            get
            {
                return userId;
            }
            set
            {
                userId = value;
            }
        }
        public bool IsListening { get { return isListening; } set { isListening = value; } }
        #endregion

        public User()
        {
            Items = new List<Item>();
            Positions = new List<Position>();            
        }

        #region Overrides
        public override string ToString()
        {
            String s = "";

            //positions
            s += $"User id: {UserId}\t\n";
            if (Positions.Count != 0)
            {
                s += $"Pozicije od usera sa id-em: {UserId}\t\n";
                foreach (Position p in Positions)
                {
                    s += p + "\n";
                }
            }
            //Items
            if (Items.Count != 0)
            {
                s += $"Itemi od usera sa id-em: {UserId}\t\n";
                foreach (Item i in Items)
                {
                    s += i + "\n";
                }
            }
            return s;
        }
        public override bool Equals(Object o)
        {
            User u = (User)o;

            if (u.UserId != this.UserId)
            {
                return false;
            }
           
            return true;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion
    }
}