declare var mindmaps: any;

// ReSharper disable InconsistentNaming
module MindMapServiceAPI {
    export function isVip(userId: number, success?: (data: any, textStatus: string, jqXHR: JQueryXHR) => any,
        error?: (jqXHR: JQueryXHR, textStatus: string, errorThrown: string) => any) {
        $.ajax({
            type: "POST",
            url: "handles/IsVip.ashx",
            data: { userId: userId },
            success: success,
            error: error,
            dataType: "json"
        });
    }

    export function getMainTitle(success?: (data: any, textStatus: string, jqXHR: JQueryXHR) => any,
        error?: (jqXHR: JQueryXHR, textStatus: string, errorThrown: string) => any) {
        $.ajax({
            type: "POST",
            url: "handles/GetMainTitle.ashx",
            success: success,
            error: error,
            dataType: "json"
        });
    }

    export function load(id: string, userId: number,
        success?: (data: any, textStatus: string, jqXHR: JQueryXHR) => any,
        error?: (jqXHR: JQueryXHR, textStatus: string, errorThrown: string) => any) {
        $.ajax({
            type: "POST",
            url: "handles/Load.ashx",
            data: { id: id, userId: userId },
            success: success,
            error: error,
            dataType: "json"
        });
    }

    export function loadDefaultDocument(userId: number,
        success?: (data: any, textStatus: string, jqXHR: JQueryXHR) => any,
        error?: (jqXHR: JQueryXHR, textStatus: string, errorThrown: string) => any) {
        $.ajax({
            type: "POST",
            url: "handles/Load.ashx",
            data: { userId: userId },
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
            url: "handles/SaveToDatabase.ashx",
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
            url: "handles/RemoveFromDatabase.ashx",
            data: { userId: doc.userId, id: doc.id },
            success: success,
            error: error,
            dataType: "json"
        });
    }

    export function getCategories(userId: number, success?: (data: any) => any, error?: (msg: string) => any) {
        $.post("handles/LoadFromDatabase.ashx",
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