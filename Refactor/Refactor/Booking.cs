using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactor
{
    public class Show
    {
        public decimal Price { get; set; }
    }
    public class Extras
    {
        public decimal PremiumFee { get; set; }
    }


    // 以委托取代子类
    public class Booking
    {
        private Show _show;
        private DateTime _date;
        public Booking(Show show, DateTime date)
        {
            _show = show;
            _date = date;
        }

        //// 演出结束后对话创作者，非高峰日提供
        //public bool HasTalkback()
        //{

        //}

        //// 定价逻辑
        //public decimal BasePrice()
        //{

        //}
    }

    public class PremiumBooking : Booking
    {
        private Show _show;
        private DateTime _date;
        private Extras _extras;
        public PremiumBooking(Show show, DateTime date, Extras extras) : base(show, date)
        {
            _extras = extras;
        }

        //// 任何一天都提供与创作者对话
        //public bool HasTalkback()
        //{

        //}

        //// 定价逻辑，用基类中的
        //public decimal BasePrice()
        //{

        //}

        //// 基类中没有的行为
        //public bool HasDinner()
        //{

        //}

    }


}
