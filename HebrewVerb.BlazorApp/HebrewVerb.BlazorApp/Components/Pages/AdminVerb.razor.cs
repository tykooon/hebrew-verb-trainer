using HebrewVerb.BlazorApp.Common;

namespace HebrewVerb.BlazorApp.Components.Pages;

public partial class AdminVerb
{
    private bool _addVerbIsOpen;
    private bool _addGizraIsOpen;
    private bool _addModelIsOpen;

    private void ToggleOpen(AdminPagePopover popover)
    {
        (_addVerbIsOpen, _addGizraIsOpen, _addModelIsOpen) = popover switch
        {
            AdminPagePopover.AddVerb => (!_addVerbIsOpen, false, false),
            AdminPagePopover.AddGizra => (false, !_addGizraIsOpen, false),
            AdminPagePopover.AddModel => (false, false, !_addModelIsOpen),
            _ => (false, false, false)
        };
    }
}
