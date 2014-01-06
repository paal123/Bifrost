describe("when visiting a plain tag", function() {
	var objectModelManager = {
		getObjectFromTagName: sinon.stub()

	};

	var visitor = Bifrost.views.ObjectModelElementVisitor.create({
		objectModelManager: objectModelManager,
		markupExtensions: {},
		typeConverters: {}	
	});

	var element = { localName: "something", attributes: [] };
	visitor.visit(element);

	it("should ask for an object by tag name", function() {
		expect(objectModelManager.getObjectFromTagName.calledWith("something")).toBe(true);
	});
});