using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using System.Diagnostics.CodeAnalysis;

namespace TestApp.Components.Controls.TextInput
{
    /// <summary>
    /// An input component for editing <see cref="string"/> values.
    /// </summary>
    public class TextInput : InputBase<string?>
    {
        /// <summary>
        /// Gets or sets the associated <see cref="ElementReference"/>.
        /// <para>
        /// May be <see langword="null"/> if accessed before the component is rendered.
        /// </para>
        /// </summary>
        [DisallowNull] public ElementReference? Element { get; protected set; }
        private string? GdsCssClass { get; set; } = string.Empty;

        protected override void OnInitialized()
        {
            GdsCssClass = "text-danger"; // Example CSS class from Bootstrap, replace with actual GDS class
            base.OnInitialized();
        }

        /// <inheritdoc />
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "input");
            builder.AddMultipleAttributes(1, AdditionalAttributes);
            if (!string.IsNullOrEmpty(NameAttributeValue))
            {
                builder.AddAttribute(2, "name", NameAttributeValue);
            }
            if (!string.IsNullOrEmpty(CssClass))
            {
                builder.AddAttribute(3, "class", GdsCssClass + " " + CssClass);
            }
            else
            {
                builder.AddAttribute(3, "class", GdsCssClass);
            }
            builder.AddAttribute(4, "value", CurrentValueAsString);
            builder.AddAttribute(5, "onchange", EventCallback.Factory.CreateBinder<string?>(this, __value => CurrentValueAsString = __value, CurrentValueAsString));
            builder.SetUpdatesAttributeName("value");
            builder.AddElementReferenceCapture(6, __inputReference => Element = __inputReference);
            builder.CloseElement();
        }

        /// <inheritdoc />
        protected override bool TryParseValueFromString(string? value, out string? result, [NotNullWhen(false)] out string? validationErrorMessage)
        {
            result = value;
            validationErrorMessage = null;
            return true;
        }
    }
}
