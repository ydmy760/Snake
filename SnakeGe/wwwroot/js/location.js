
function getloc() {
	$.getJSON("https://api.ipify.org/?format=jsonp&callback=?", function (data) {
		var ip = data.ip;
		$.getJSON("https://restapi.amap.com/v3/ip?ip=" + ip + "&key=83eeb2be5cccd3a740a549e7ade758e8", function (data) {
			var province = data.province;
			var city = data.city;
			document.getElementById('aaa').value = province;
			click();
		})
	})
}
function click1(province) {
	$.post('/Location/Getinfo', { province: province }, function (result) {
		if (result[0][0] == "wrong") {
			alert("抱歉，暂时查不到数据哦");
		}
		else {
			var html = '<div id="abcd">'
			for (var i = 0; i < result.length; i++) {
				html += '<div style="clear:both;font-family:KaiTi">' + result[i][2] + '</div>';
				html += '</br>';
				html += '<div style="clear:both">' + '记录编号:' + '</div>' + '<div>' + result[i][0] + '&ensp;</div>';
				html += '<div style="clear:both">' + '种名:' + '</div>' + '<div>' + result[i][6] + '&ensp;</div>';
				html += '<div style="clear:both">' + '别名:' + '</div>' + '<div>' + result[i][7] + '&ensp;</div>';
				html += '<div>' + '目:' + '</div>' + '<div>' + result[i][3] + '&ensp;</div>';
				html += '<div>' + '科:' + '</div>' + '<div>' + result[i][4] + '&ensp;</div>';
				html += '<div>' + '属:' + '</div>' + '<div>' + result[i][5] + '&ensp;</div>';
				html += '<div>' + '是否有毒:' + '</div>' + '<div>' + result[i][8] + '&ensp;</div>';
				html += '</br>';
				html += '<div style="clear:both">' + '简介:' + '</div>' + '<div>' + result[i][9] + '</div>';
				html += '</br>';
				html += '</br>';
				html += '</br>';
			}
			html += '</div>'

			$("#abcd").html(html);
		}
	})
}
function click() {
	var province = document.getElementById('aaa').value;
	click1(province);
	$.post('/Location/GetSnake', { province: province }, function (result) {
		Draw(province, result);
	})
	document.getElementById('pro').value = province;
}

function get() { 
	click();
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
		var marker = new BMap.Marker(poi.point);
		map.addOverlay(marker);
		//marker.setTitle(i);
		//marker.onclick = function (e) { alert(this.getTitle()); }

		marker.setAnimation(BMAP_ANIMATION_BOUNCE); //跳动的动画
	}
}

function change() {
	var selec = document.getElementById('div_1').value;
	$.post('/Location/Getinfo2', { id: selec }, function (result) {
		var html = '<div>'
		for (var i = 0; i < result.length; i++) {
			document.getElementById('input1').value = result[i][0];
			document.getElementById('input2').value = result[i][6];
			document.getElementById('input8').value = result[i][7];
			document.getElementById('input3').value = result[i][3];
			document.getElementById('input4').value = result[i][4];
			document.getElementById('input5').value = result[i][5];
			if (result[i][8] == "有毒") {
				$('#input6_1').prop("checked", true);
			}
			else if (result[i][8] == "无毒") {
				$('#input6_2').prop("checked", true);
			}
			document.getElementById('input7').value = result[i][9];
			/*			html += '<div style="clear:both;font-family:KaiTi">' + result[i][7] + '</div>';
						html += '</br>';
						html += '<div style="clear:both">' + '记录编号:' + '</div>' + '<div>' + result[i][0] + '&ensp;</div>';
						html += '<div style="clear:both">' + '种名:' + '</div>' + '<div>' + result[i][4] + '&ensp;</div>';
						html += '<div>' + '目:' + '</div>' + '<div>' + result[i][1] + '&ensp;</div>';
						html += '<div>' + '科:' + '</div>' + '<div>' + result[i][2] + '&ensp;</div>';
						html += '<div>' + '属:' + '</div>' + '<div>' + result[i][3] + '&ensp;</div>';
						html += '<div>' + '是否有毒:' + '</div>' + '<div>' + result[i][5] + '&ensp;</div>';
						html += '</br>';
						html += '<div style="clear:both">' + '简介:' + '</div>' + '<div>' + result[i][8] + '</div>';
						html += '</br>';
						html += '</br>';
						html += '</br>';
					}
					html += '</div>'
					*/
		}
		
	})
}
