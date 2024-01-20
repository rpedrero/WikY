let articleDeleteModal = new bootstrap.Modal(document.getElementById('delete-article-modal'), {
    keyboard: false
});

function deleteArticle(id) {
    window.location.href = '/Article/Delete/' + id;
}