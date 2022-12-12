#
# Module manifest for module 'ProductivityTools.PSDisplayPosition'
#
# Generated by: pwujczyk
#
# Generated on: 18.05.2020
#

@{

# Script module or binary module file associated with this manifest.
RootModule = 'ProductivityTools.TeamManagement.Cmdlet.dll'

# Version number of this module.
ModuleVersion = '0.0.10'

# ID used to uniquely identify this module
GUID = '0b614158-0823-4edb-ad35-a4b6e2e6449b'

# Author of this module
Author = 'Pawel wujczyk'

# Description of the functionality provided by this module
Description = 'It is a cmdlet which allows to save quick feedbacks about the team'

# Cmdlets to export from this module, for best performance, do not use wildcards and do not delete the entry, use an empty array if there are no cmdlets to export.
CmdletsToExport = @('Get-Feedback','Set-Feedback','Add-PersonFeedback','Get-PeopleList')

# List of all files packaged with this module
FileList=@(
'Newtonsoft.Json.dll',
'ProductivityTools.DescriptionValue.dll',
'ProductivityTools.PSCmdlet.dll',
'ProductivityTools.SimpleHttpPostClient.dll',
'ProductivityTools.TeamManagement.Cmdlet.ClientCaller.dll',
'ProductivityTools.TeamManagement.Cmdlet.ClientCaller.pdb',
'ProductivityTools.TeamManagement.Cmdlet.deps.json',
'ProductivityTools.TeamManagement.Cmdlet.dll',
'ProductivityTools.TeamManagement.Cmdlet.pdb',
'ProductivityTools.TeamManagement.Contract.dll',
'ProductivityTools.TeamManagement.psd1',
'System.Management.Automation.dll'
)

# Private data to pass to the module specified in RootModule/ModuleToProcess. This may also contain a PSData hashtable with additional module metadata used by PowerShell.
PrivateData = @{

    PSData = @{

        # Tags applied to this module. These help with module discovery in online galleries.
        Tags = @('Team','Management')

        # A URL to the license for this module.
        # LicenseUri = ''

        # A URL to the main website for this project.
        ProjectUri = 'http://productivitytools.tech/display-position/'

        # A URL to an icon representing this module.
        IconUri = 'http://cdn.productivitytools.tech/images/PT/ProductivityTools_blue_85px_3.png'

        # ReleaseNotes of this module
        # ReleaseNotes = ''

    } # End of PSData hashtable

} # End of PrivateData hashtable

# HelpInfo URI of this module
 HelpInfoURI = 'http://productivitytools.tech/display-position/'

}

