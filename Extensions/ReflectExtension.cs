namespace FastBite.Extensions
{
    public static class ReflectExtension
    {
        public static string GetPropertyValue<T>(this T item,string propertyName){
            return item.GetType().GetProperty(propertyName).GetValue(item,null).ToString();
        }
    }
}