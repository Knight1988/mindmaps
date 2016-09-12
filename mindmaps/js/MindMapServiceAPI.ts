declare var mindmaps: any;

// ReSharper disable InconsistentNaming
module MindMapServiceAPI {
    export function load(id: string, success?: (data: any) => any, error?: (msg: string) => any) {
        $.post("LoadFromDatabase.ashx",
            { id: id },
            (result: MindMapResult) => {
                if (result.success && typeof (success) === "function") {
                    // success
                    success(result.data);
                } else {
                    // error
                    error(result.message);
                }
            });
    }

    export function save(id: string,
        userId: number,
        title: string,
        data: string,
        success?: () => any,
        error?: (msg: string) => any) {
        const obj = { id, userId, title, data };
        $.post("SaveToDatabase.ashx",
            obj,
            (result: MindMapResult) => {
                if (result.success && typeof (success) === "function") {
                    // success
                    success();
                } else {
                    // error
                    error(result.message);
                }
            });
    }

    export function remove(doc: any, success?: (data: any) => any, error?: (msg: string) => any) {
        $.post("RemoveFromDatabase.ashx",
            { id: doc.id },
            (result: MindMapResult) => {
                if (result.success && typeof (success) === "function") {
                    const datas = [];
                    // get the document list
                    for (let i = 0; i < result.data.length; i++) {
                        const data = result.data[i];
                        datas.push(mindmaps.Document.fromJSON(data.data));
                    }
                    // success
                    success(datas);
                } else if (typeof (success) === "function") {
                    // error
                    error(result.message);
                }
            });
    }

    export function getlist(userId: number, success?: (data: any) => any, error?: (msg: string) => any) {
        $.post("LoadFromDatabase.ashx",
            { userId: userId },
            (result: MindMapResult) => {
                if (result.success && typeof (success) === "function") {
                    const datas = [];
                    // get the document list
                    for (let i = 0; i < result.data.length; i++) {
                        const data = result.data[i];
                        var doc = mindmaps.Document.fromJSON(data.data);
                        doc.canEdit = data.canEdit;
                        datas.push(doc);
                    }
                    // success
                    success(datas);
                } else {
                    // error
                    error(result.message);
                }
            });
    }
}
// ReSharper restore InconsistentNaming