function AuthorIndexViewModel(authors) {
    var self = this;
    self.authors = authors;

    self.showDeleteModal = function (data, event) {
        self.sending = ko.observable(false);
        console.log(data);
        console.log(event);
        $.get($(event.target).attr('href', function (d) {
            $(body).prepend(d);
            $('#deleteModal').modal('show');

            ko.applyBindings(self, document.getElementById('deleteModal'));
        }))
    }

    self.deleteAuthor = function (form) {
        self.sending(true);
        return true;
    }
}