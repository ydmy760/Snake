
function getloc() {
	$.getJSON("https://api.ipify.org/?format=jsonp&callback=?", function (data) {
		var ip = data.ip;
		$.getJSON("https://restapi.amap.com/v3/ip?ip=" + ip + "&key=83eeb2be5cccd3a740a549e7ade758e8", function (data) {
			var province = data.province;
			var city = data.city;
			$.post('/Location/GetSnake', { province: province }, function (result) {
				Draw(province, result);
				document.getElementById('aaa').value = province;
			})
            });
	});
}
function Draw(cityName, alist) {
	var map = new BMap.Map("divMap");
	map.centerAndZoom(new BMap.Point(116.404, 39.915), 11);
	map.enableScrollWheelZoom();
	var city = new BMap.LocalSearch(map, { renderOptions: { map: map, autoViewport: true } });
	map.clearOverlays();
	city.search(cityName);
	var ls = new BMap.LocalSearch(cityName);
	var label = new BMap.Label(cityName + "蛇类位置图");  // 创建文本标注对象
	label.setStyle({
		color: 'red',
		fontSize: '12px',
		height: '20px',
		lineHeight: '20px',
		fontFamily: '微软雅黑'
	});
	map.addOverlay(label);
	for (i = 0; i < alist.length; i++) {
		ls.search(alist[i]);
		ls.setSearchCompleteCallback(createCallback(map,alist[i]));
	}
}
function createCallback(map, alist) {
	return function (rs) {
		var poi = rs.getPoi(0);
		alert(alist);
		var marker = new BMap.Marker(poi.point);
		map.addOverlay(marker);
		//marker.setTitle(i);
		//marker.onclick = function (e) { alert(this.getTitle()); }

		marker.setAnimation(BMAP_ANIMATION_BOUNCE); //跳动的动画
	}
}