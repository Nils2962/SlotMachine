using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino.Classes
{
    public class Profile
    {
        public int RfidSerialnumber { get => rfidSerialnumber; }
        public double Credit { get => credit; }

        public delegate void DelCreditChanged(object sender, double newcredit);
        public event DelCreditChanged CreditChanged;

        private double credit;
        private int rfidSerialnumber;

        public Profile(int rfidSerialnumber)
        {
            this.rfidSerialnumber = rfidSerialnumber;
            this.credit = 0;
        }

        public void AddCredit(double money)
        {
            this.credit += money;
            CreditChanged?.Invoke(this, this.credit);
        }

        public bool CanRoll(double commitment)
        {
            if(this.credit - commitment >= 0)
            {
                return true;
            }

            return false;
        }

        public void RemoveCommitment(double commitment)
        {
            this.credit -= commitment;
            CreditChanged?.Invoke(this, this.credit);
        }
    }
}
