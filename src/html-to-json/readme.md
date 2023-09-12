### 1. 미러링

`wget`을 사용하여 특정 웹사이트를 미러링. 이 명령은 웹사이트의 페이지들을 재귀적으로 다운로드.

```bash
wget --recursive --no-clobber --page-requisites --html-extension --convert-links --no-parent --restrict-file-names=windows --reject-regex '\?' --user-agent="Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.3" https://{domain-name}
```

### 2. HTML 파일 추출
미러링된 파일에서 .html 확장자를 가진 파일들만 추출 후 다른 위치(./html/ 디렉토리)로 이동.

```bash
find ./{domain-name} -type f -name "*.html" -exec mv {} ./html/ \;
```

[참고] 위 프로세스는 robots.txt를 무시하지않는다.