let commentDeleteModal = new bootstrap.Modal(document.getElementById('delete-comment-modal'), {
    keyboard: false
});

let selectedCommentId = null;

function unselectCurrentComment() {
    selectedCommentId = null;
}

function deleteComment() {
    window.location.href = '/Comment/Delete/' + selectedCommentId;
}

function onCommentDeleteButton(commentId) {
    selectedCommentId = commentId;

    commentDeleteModal.show();
}
