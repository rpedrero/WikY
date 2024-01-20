let searchByTopicInputElement = $('#search-by-topic-input');
let searchByContentInputElement = $('#search-by-content-input');
let searchByAuthorInputElement = $('#search-by-author-input');

let articlesTableElement = $('#articles-list');

searchByTopicInputElement.on('input', onSearchFieldInput);
searchByContentInputElement.on('input', onSearchFieldInput);
searchByAuthorInputElement.on('input', onSearchFieldInput);

function onSearchFieldInput(e) {
    $.ajax({
        url: '/Article/SearchAjax/',
        type: 'GET',
        data: {
            topic: searchByTopicInputElement.val(),
            content: searchByContentInputElement.val(),
            author: searchByAuthorInputElement.val(),
        },
        success: function (data) {
            articlesTableElement.html(data);
        }
    });
}

