(function (ko, datacontext) {
	datacontext.tolistItem = tolistItem;
	datacontext.toOrderNote = toOrderNote;
	datacontext.toOrder = toOrder;

	function tolistItem(data) {
		var self = this;
		data = data || {};

		// Persisted properties
		self.lineItemId = data.lineItemId;
		self.orderId = data.orderId;
		self.imageUrl = ko.observable(data.imageUrl);
		self.amazonUrl = ko.observable(data.amazonUrl);
		self.description = ko.observable(data.description);
		self.quantity = ko.observable(data.quantity);
		self.estimatedCost = ko.observable(data.estimatedCost);
		self.actualCost = ko.observable(data.actualCost);
		self.notes = ko.observable(data.notes);
		self.removed = ko.observable(data.removed);

		// Non-persisted properties
		self.errorMessage = ko.observable();

		saveChanges = function () {
			return datacontext.saveChangedLineItem(self);
		};

		// Auto-save when these properties change
		self.imageUrl.subscribe(saveChanges);
		self.amazonUrl.subscribe(saveChanges);
		self.description.subscribe(saveChanges);
		self.quantity.subscribe(saveChanges);
		self.estimatedCost.subscribe(saveChanges);
		self.actualCost.subscribe(saveChanges);
		self.notes.subscribe(saveChanges);
		self.removed.subscribe(saveChanges);

		self.toJson = function () { return ko.toJSON(self) };
	};

	function toOrderNote(data) {
		var self = this;
		data = data || {};

		// Persisted properties
		self.orderNoteId = data.orderNoteId;
		self.orderId = data.orderId;
		self.noteText = data.noteText;
		self.createdDate = datra.createdDate;
	};

})(ko, todoApp.datacontext);