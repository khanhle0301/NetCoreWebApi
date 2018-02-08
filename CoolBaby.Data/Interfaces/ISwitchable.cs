using CoolBaby.Data.Enums;

namespace CoolBaby.Data.Interfaces
{
    public interface ISwitchable
    {
        Status Status { set; get; }
    }
}