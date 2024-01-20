let globalFormErrorElement = $('#ajax-global-form-error-placeholder');

let articleIdInputElement = $('#ajax-article-id-input');
let authorInputElement = $('#ajax-author-input');
let contentInputElement = $('#ajax-content-input');

let ajaxAuthorValidationElement = $('#ajax-author-validation');
let ajaxContentValidationElement = $('#ajax-content-validation');

$('#create-comment-ajaxversion-form').submit(function (e) {
    e.preventDefault();

    $.post('/Comment/CreateAjax',
        {
            ArticleId: articleIdInputElement.val(),
            Author: authorInputElement.val(),
            Content: contentInputElement.val()
        },
        function (data) {
            let noCommentTextElement = $('#no-comment');
            if (noCommentTextElement) {
                noCommentTextElement.remove();
            }

            $('#article-comments').prepend(data);

            authorInputElement.val(null);
            contentInputElement.val(null);

            globalFormErrorElement.children().remove();
            globalFormErrorElement.addClass('hidden');
        })
        .fail(function (response) {
            if (response.status === 400) {
                let responseJSON = response.responseJSON;

                if (responseJSON['Author']) {
                    ajaxAuthorValidationElement.text(responseJSON['Author']);
                    ajaxAuthorValidationElement.removeClass('hidden');
                }

                if (responseJSON['Content']) {
                    ajaxContentValidationElement.text(responseJSON['Content']);
                    ajaxContentValidationElement.removeClass('hidden');
                }
            }
            else if (response.status === 500) {
                globalFormErrorElement.append('<p>An unexpected error has occurred when attempting to send your comment. Try again later.</p>');
                globalFormErrorElement.removeClass('hidden');
            }
        });
});

authorInputElement.on('input', function (e) {
    if (!e.target.value.trim()) {
        ajaxAuthorValidationElement.text('The Author field is required.');
        ajaxAuthorValidationElement.removeClass('hidden');
    }
    else {
        ajaxAuthorValidationElement.text(null);
        ajaxAuthorValidationElement.addClass('hidden');

        if (e.target.value.length > 30) {
            ajaxAuthorValidationElement.text('The field Author must be a string or array type with a maximum length of \'30\'.');
            ajaxAuthorValidationElement.removeClass('hidden');
        }
        else {
            ajaxAuthorValidationElement.text(null);
            ajaxAuthorValidationElement.addClass('hidden');
        }
    }
});

contentInputElement.on('input', function (e) {
    if (!e.target.value.trim()) {
        ajaxContentValidationElement.text('The Content field is required.');
        ajaxContentValidationElement.removeClass('hidden');
    }
    else {
        ajaxContentValidationElement.text(null);
        ajaxContentValidationElement.addClass('hidden');

        if (e.target.value.length > 1000) {
            ajaxContentValidationElement.text('The field Content must be a string or array type with a maximum length of \'1000\'.');
            ajaxContentValidationElement.removeClass('hidden');
        }
        else {
            ajaxContentValidationElement.text(null);
            ajaxContentValidationElement.addClass('hidden');
        }
    }
});
