public abstract class Element {
    public string label;
    public bool   color;

    public Element(string label, bool color) {
        this.label = label;
        this.color = color;
    }

    public void paint() {
        color = true;
    }

    public void alternPaint() {
        color = false;
    }

    public bool isPainted() {
        return color;
    }

    public bool isNotPainted() {
        return !color;
    }
}