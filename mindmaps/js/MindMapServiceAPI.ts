// ReSharper disable InconsistentNaming
module MindMapServiceAPI {
    export function load(id: string, success?: (data: any) => any, error?: (msg: string) => any) {
        $.post("LoadFromDatabase.ashx", { id: id }, (result: MindMapResult) => {
            if (result.success && typeof (success) === "function") {
                // success
                success(result.data);
            } else {
                // error
                error(result.message);
            }
        });
    }

    export function save(id: string, userId: number, title: string, data: string, success?: () => any, error?: (msg: string) => any) {
        var obj = { id, userId, title, data };
        $.post("SaveToDatabase.ashx", obj, (result: MindMapResult) => {
            if (result.success && typeof (success) === "function") {
                // success
                success();
            } else {
                // error
                error(result.message);
            }
        });
    }

    export function getlist(userId: number, success?: (data: any) => any, error?: (msg: string) => any) {
        $.post("GetList.ashx", { userId: userId }, (result: MindMapResult) => {
            if (result.success && typeof (success) === "function") {
                // success
                success(result.data);
            } else {
                // error
                error(result.message);
            }
        });
    }
}
// ReSharper restore InconsistentNaming