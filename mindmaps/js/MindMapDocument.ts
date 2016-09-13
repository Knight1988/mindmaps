class MindMapDocument {
    id: string;
    categoryId: number;
    userId: number;
    title: string;
    modifiedDate: string;
    data: string;
}

class MindMapCategory {
    id: number;
    name: string;
    documents: MindMapDocument[];
}