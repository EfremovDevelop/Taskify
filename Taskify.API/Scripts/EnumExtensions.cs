using System.ComponentModel;
using System.Reflection;

namespace Taskify.API.Scripts
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());

            if (field != null)
            {
                DescriptionAttribute attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));

                if (attribute != null)
                {
                    return attribute.Description;
                }
            }

            return value.ToString(); // Возвращаем строковое представление значения перечисления, если атрибут Description не найден
        }
    }
}
