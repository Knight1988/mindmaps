declare var mindmaps: any;

// ReSharper disable InconsistentNaming
module MindMapServiceAPI {
    export function load(id: string,
        success?: (data: any, textStatus: string, jqXHR: JQueryXHR) => any,
        error?: (jqXHR: JQueryXHR, textStatus: string, errorThrown: string) => any) {
        $.ajax({
            type: "POST",
            url: "LoadFrom.ashx",
            data: { id: id },
            success: success,
            error: error,
            dataType: "json"
        });
    }

    export function save(userId: number, doc: any,
        success?: (data: any, textStatus: string, jqXHR: JQueryXHR) => any,
        error?: (jqXHR: JQueryXHR, textStatus: string, errorThrown: string) => any) {

        $.ajax({
            type: "POST",
            url: "SaveToDatabase.ashx",
            data: {
                userId: userId,
                doc: doc.serialize()
            },
            success: success,
            error: error,
            dataType: "json"
        });
    }

    export function remove(doc: any,
        success?: (data: any, textStatus: string, jqXHR: JQueryXHR) => any,
        error?: (jqXHR: JQueryXHR, textStatus: string, errorThrown: string) => any) {
        $.ajax({
            type: "POST",
            url: "RemoveFromDatabase.ashx",
            data: { userId: doc.userId, id: doc.id },
            success: success,
            error: error,
            dataType: "json"
        });
    }

    export function getCategories(userId: number, success?: (data: any) => any, error?: (msg: string) => any) {
        $.post("LoadFromDatabase.ashx",
            { userId: userId },
            (result: MindMapCategory) => {
                const categories = parseData(userId, result);
                // success
                success(categories);
            });
    }

    function parseData(userId: number, datas: any): MindMapCategory[] {
        const categories: MindMapCategory[] = [];
        // get the document list
        for (let i = 0; i < datas.length; i++) {
            // get category
            const category: MindMapCategory = mindmaps.Category.fromObject(datas[i]);
            categories.push(category);
        }
        return categories;
    }
}
// ReSharper restore InconsistentNaming