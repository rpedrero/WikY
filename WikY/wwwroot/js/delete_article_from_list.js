let articleDeleteModal = new bootstrap.Modal(document.getElementById('delete-article-modal'), {
    keyboard: false
});

let selectedArticleId = null;

function unselectCurrentArticle() {
    selectedArticleId = null;
}

function showDeleteModal(articleId) {
    selectedArticleId = articleId;

    articleDeleteModal.show();
}

function deleteSelectedArticle() {
    $.ajax({
        url: '/Article/DeleteJson/' + selectedArticleId,
        type: 'DELETE',
        success: function (result) {
            $('#article-' + selectedArticleId).remove();
        },

    }).done(function () {
        unselectCurrentArticle();
    });
}
