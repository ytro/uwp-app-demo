namespace PCApplication.Converters
{
    /// <summary>
    /// A boolean to negated boolean IValueConverter
    /// </summary>
    public class InvertedBooleanConverter : BooleanConverterBase<bool>
    {
        public InvertedBooleanConverter()
            : base(trueValue: false, falseValue: true)
        {
        }
    }
}
