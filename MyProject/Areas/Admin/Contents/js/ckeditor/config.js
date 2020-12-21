/**
 * @license Copyright (c) 2003-2018, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see https://ckeditor.com/legal/ckeditor-oss-license
 */

CKEDITOR.editorConfig = function (config) {
    // Define changes to default configuration here. For example:
    // config.language = 'fr';
    // config.uiColor = '#AADC6E';

    config.syntaxhighlight_lang = 'csharp';
    config.syntaxhighlight_hideControls = true;
    config.language = 'vi';
    config.filebrowserBrowseUrl = '/Areas/Admin/Contents/js/ckfinder/ckfinder.html';
    config.filebrowserImageBrowseUrl = '/Areas/Admin/Contents/js/ckfinder.html?Type=Images';
    config.filebrowserFlashBrowseUrl = '/Areas/Admin/Contents/js/ckfinder.html?Type=Flash';
    config.filebrowserUploadUrl = '/Areas/Admin/Contents/js/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';
    config.filebrowserImageUploadUrl = '/ImageData';
    config.filebrowserFlashUploadUrl = '/Areas/Admin/Contents/js/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';

    CKFinder.setupCKEditor(null, '/Areas/Admin/Contents/js/ckfinder/');
};
