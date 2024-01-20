let commentDeleteAjaxModal = new bootstrap.Modal(document.getElementById('delete-comment-ajax-modal'), {
    keyboard: false
});

let selectedCommentIdAjax = null;

function unselectCurrentCommentAjax() {
    selectedCommentIdAjax = null;
}

function deleteCommentAjax() {
    $.ajax({
        url: '/Comment/DeleteAjax/' + selectedCommentIdAjax,
        type: 'DELETE',
        success: function (result) {
            $('#comment-' + selectedCommentIdAjax).remove();
        },
    }).done(function () {
        unselectCurrentComment();
    });
}

function onCommentAjaxDeleteButton(commentId) {
    selectedCommentIdAjax = commentId;

    commentDeleteAjaxModal.show();
}
