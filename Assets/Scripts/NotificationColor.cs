using System.ComponentModel;

namespace Logic
{
    public enum NotificationColor
    {
        [Description("Blue")]
        Telegram,
        [Description("Green")]
        WhatsApp,
        [Description("Yellow")]
        YandexPost,
        [Description("Red")]
        YouTube,
		[Description("White")]
		Silent,
	}
}
