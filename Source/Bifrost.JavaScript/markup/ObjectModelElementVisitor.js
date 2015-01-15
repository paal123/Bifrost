Bifrost.namespace("Bifrost.markup", {
    ObjectModelElementVisitor: Bifrost.markup.ElementVisitor.extend(function(objectModelManager, markupExtensions, typeConverters) {
        this.visit = function(element, actions) {
            // Tags : 
            //  - tag names automatically match type names
            //  - due to tag names in HTML elements being without case - they become lower case in the
            //    localname property, we will have to search for type by lowercase
            //  - multiple types found with different casing in same namespace should throw an exception
            // Namespaces :
            //  - split by ':'
            //  - if more than one ':' - throw an exception
            //  - if no namespace is defined, try to resolve in the global namespace
            //  - namespaces in the object model can point to multiple JavaScript namespaces
            //  - multiple types with same name in namespace groupings should throw an exception
            //  - registering a namespace can be done on any tag by adding xmlns:name="point to JS namespace"
            //  - If one registers a namespace with a prefix a parent already has and no naming root sits in between, 
            //    it should add the namespace target on the same definition
            //  - Naming roots are important - if there occurs a naming root, everything is relative to that and 
            //    breaking any "inheritance"
            // Properties : 
            //  - Attributes on an element is a property
            //  - Values in property should always go through type conversion sub system
            //  - Values with encapsulated in {} should be considered markup extensions, go through 
            //    markup extension system for resolving them and then pass on the resulting value 
            //    to type conversion sub system
            //  - Properties can be set with tag suffixed with .<name of property> - more than one
            //    '.' in a tag name should throw an exception
            // Child tags :
            //  - Children which are not a property reference are only allowed if a content or
            //    items property exist. There can only be one of the other, two of either or both
            //    at the same time should yield an exception
            // Markup extensions :
            //  - Any value should be recognized when it is a markup extension
            //
            // Example : 
            // Simple control:
            // <somecontrol property="42"/>
            // 
            // Control in different namespace:
            // <ns:somecontrol property="42"/>
            //
            // Assigning property with tags:
            // <ns:somecontrol>
            //    <ns:somecontrol.property>42</ns:somcontrol.property>
            // </ns:somecontrol>
            // 
            // Using a markup extension:
            // <ns:somecontrol somevalue="{{binding property}}">
            // <ns:somecontrol
            //
            // <span>{{binding property}}</span>
            //
            // <ns:somecontrol>
            //    <ns:somecontrol.property>{{binding property}}</ns:somcontrol.property>
            // </ns:somecontrol>

            if (element.isKnownType()) {
                return;
            }

            var namespace;
            var name = element.localName.toLowerCase();

            var namespaceSplit = name.split(":");
            if ( namespaceSplit.length > 2 ) {
                throw Bifrost.markup.MultipleNamespacesInNameNotAllowed.create({ tagName: name });
            }
            if ( namespaceSplit.length === 2 ) {
                name = namespaceSplit[1];
                namespace = namespaceSplit[0];
            }

            var instance = objectModelManager.getObjectFromTagName(name, namespace);
            if (Bifrost.isNullOrUndefined(instance)) {
                return;
            }
            element.__objectModelNode = instance;

            var propertySplit = element.localName.split(".");
            if( propertySplit.length > 2 ) {
                throw Bifrost.markup.MultiplePropertyReferencesNotAllowed.create({ tagName: name });
            }

            if( propertySplit.length === 2 ) {
                if( !Bifrost.isNullOrUndefined(element.parentElement) ) {
                    var parentName = element.parentElement.localName.toLowerCase();

                    if( parentName !== propertySplit[0] ) {
                        throw Bifrost.markup.ParentTagNameMismatched.create({ tagName: name, parentTagName: parentName });
                    }
                }
            }

            if( !Bifrost.isNullOrUndefined(element.parentElement) ) {
                propertySplit = element.parentElement.localName.split(".");
                if( propertySplit.length === 2 ) {
                    var propertyName = propertySplit[1];
                    if( !Bifrost.isNullOrUndefined(element.parentElement.__objectModelNode) ) {
                        if( ko.isObservable(element.parentElement.__objectModelNode[propertyName]) ) {
                            element.parentElement.__objectModelNode[propertyName](instance);
                        } else {
                            element.parentElement.__objectModelNode[propertyName] = instance;
                        }
                    }
                }
            }

            for( var attributeIndex=0; attributeIndex<element.attributes.length; attributeIndex++ ) {
                name = element.attributes[attributeIndex].localName;
                var value = element.attributes[attributeIndex].value;

                if( name in instance ) {
                    var targetValue = instance[name];
                    var targetType = typeof targetValue;
                    if( ko.isObservable(targetValue)) {
                        targetType = typeof targetValue();
                    }
                    var convertedValue = typeConverters.convert(targetType, value);
                    if( ko.isObservable(targetValue)) {
                        targetValue(convertedValue);
                    } else {
                        instance[name] = convertedValue;
                    }
                }
            }
        };
    })
});