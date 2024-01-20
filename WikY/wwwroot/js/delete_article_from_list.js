let articleDeleteModal = new bootstrap.Modal(document.getElementById('delete-article-modal'), {
    keyboard: false
});

let modalArticleToDeleteTitleElement = $('#article-to-delete-title');

let selectedArticleId = null;

function unselectCurrentArticle() {
    selectedArticleId = null;
    modalArticleToDeleteTitleElement.text('');
}

function showDeleteModal(articleId) {
    selectedArticleId = articleId;

    modalArticleToDeleteTitleElement.text($('#article-' + articleId + '>td>a').text());

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
