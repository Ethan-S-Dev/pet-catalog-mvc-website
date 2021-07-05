$(function ()
{
    $("input:file.custom-file-input").change(function () {
        if (this.files && this.files[0]) {

            var reader = new FileReader();
            reader.onload = function (e) {
                $(".selected-image").attr('src', e.target.result);
            }

            reader.readAsDataURL(this.files[0]);
        }
        
        
    });
});