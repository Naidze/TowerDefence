export function distance(x0, y0, x1, y1) {
    return Math.sqrt(Math.pow(x1 - x0, 2) + Math.pow(y1 - y0, 2));
}

function dist2(v, w) {
    return Math.pow(v[0] - w[0], 2) + Math.pow(v[1] - w[1], 2);
}

function distToSegmentSquared(p, v, w) {
    var l2 = dist2(v, w);
    if (l2 === 0) return dist2(p, v);
    var t = ((p[0] - v[0]) * (w[0] - v[0]) + (p[1] - v[1]) * (w[1] - v[1])) / l2;
    t = Math.max(0, Math.min(1, t));
    return dist2(p, [v[0] + t * (w[0] - v[0]), v[1] + t * (w[1] - v[1])]);
}

export function distanceToLineSegment(x0, y0, x1, y1, x2, y2) {
    return Math.sqrt(distToSegmentSquared([x0, y0], [x1, y1], [x2, y2]));
}
